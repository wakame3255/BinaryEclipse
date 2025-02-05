using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class UnusedAssetsCleaner : EditorWindow
{
    private List<string> selectedFolders = new List<string>();
    private Vector2 scrollPosition;
    private List<string> unusedAssets = new List<string>();
    private bool showConfirmation = false;

    [MenuItem("Tools/���g�p�A�Z�b�g�N���[�i�[")]
    static void ShowWindow()
    {
        GetWindow<UnusedAssetsCleaner>("���g�p�A�Z�b�g�N���[�i�[");
    }

    private void OnGUI()
    {
        GUILayout.Label("���g�p�A�Z�b�g�N���[�i�[", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // �t�H���_�I�𕔕�
        GUILayout.Label("�����Ώۃt�H���_:");

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));

        for (int i = 0; i < selectedFolders.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(selectedFolders[i]);
            if (GUILayout.Button("�폜", GUILayout.Width(60)))
            {
                selectedFolders.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("�t�H���_��ǉ�"))
        {
            string folder = EditorUtility.OpenFolderPanel("��������t�H���_��I��", "Assets", "");
            if (!string.IsNullOrEmpty(folder))
            {
                string relativePath = "Assets" + folder.Substring(Application.dataPath.Length);
                if (!selectedFolders.Contains(relativePath))
                {
                    selectedFolders.Add(relativePath);
                }
            }
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("���g�p�A�Z�b�g������"))
        {
            FindUnusedAssets();
        }

        // ���g�p�A�Z�b�g�\������
        if (unusedAssets.Count > 0)
        {
            EditorGUILayout.Space();
            GUILayout.Label($"���g�p�A�Z�b�g ({unusedAssets.Count}��):");

            EditorGUILayout.BeginScrollView(Vector2.zero);
            foreach (string asset in unusedAssets)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(asset);
                if (GUILayout.Button("�I��", GUILayout.Width(60)))
                {
                    Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(asset);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            if (!showConfirmation)
            {
                if (GUILayout.Button("�I���������g�p�A�Z�b�g���폜"))
                {
                    showConfirmation = true;
                }
            }
            else
            {
                EditorGUILayout.HelpBox("�{���ɍ폜���܂����H���̑���͎������܂���B", MessageType.Warning);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("�͂��A�폜���܂�"))
                {
                    DeleteUnusedAssets();
                    showConfirmation = false;
                }
                if (GUILayout.Button("�L�����Z��"))
                {
                    showConfirmation = false;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void FindUnusedAssets()
    {
        if (selectedFolders.Count == 0)
        {
            EditorUtility.DisplayDialog("�G���[", "�t�H���_��I�����Ă��������B", "OK");
            return;
        }

        unusedAssets.Clear();

        // ���ׂĂ̈ˑ��֌W���擾
        string[] allDependencies = AssetDatabase.GetAllAssetPaths()
            .SelectMany(path => AssetDatabase.GetDependencies(path, true))
            .Distinct()
            .ToArray();

        // �I�����ꂽ�t�H���_���̃A�Z�b�g���`�F�b�N
        foreach (string folder in selectedFolders)
        {
            string[] assets = AssetDatabase.FindAssets("", new[] { folder })
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .Where(path => !path.EndsWith(".cs") && !path.EndsWith(".meta"))
                .ToArray();

            foreach (string asset in assets)
            {
                if (!allDependencies.Contains(asset))
                {
                    unusedAssets.Add(asset);
                }
            }
        }

        if (unusedAssets.Count == 0)
        {
            EditorUtility.DisplayDialog("����", "���g�p�̃A�Z�b�g�͌�����܂���ł����B", "OK");
        }
    }

    private void DeleteUnusedAssets()
    {
        foreach (string asset in unusedAssets)
        {
            AssetDatabase.DeleteAsset(asset);
        }

        AssetDatabase.Refresh();
        unusedAssets.Clear();
        EditorUtility.DisplayDialog("����", "�I�����ꂽ���g�p�A�Z�b�g���폜���܂����B", "OK");
    }
}