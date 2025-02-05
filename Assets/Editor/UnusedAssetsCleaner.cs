using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnusedAssetsCleaner : EditorWindow
{
    // �I�����ꂽ�t�H���_�iAssets�ȉ��̃p�X�j
    private List<string> selectedFolders = new List<string>();

    // �X�L�������ʁF�g���Ă��Ȃ��A�Z�b�g�̃p�X�ꗗ
    private List<string> unusedAssets = new List<string>();

    // �X�L���������s���ꂽ���ǂ���
    private bool scanPerformed = false;

    // �X�N���[���r���[�p�̃X�N���[���ʒu
    private Vector2 scrollPos;

    [MenuItem("Tools/Unused Assets Cleaner")]
    public static void ShowWindow()
    {
        GetWindow<UnusedAssetsCleaner>("���g�p�A�Z�b�g�폜�G�f�B�^");
    }

    private void OnGUI()
    {
        // �Ώۃt�H���_�̑I���敪
        EditorGUILayout.LabelField("�y�Ώۃt�H���_�I���z", EditorStyles.boldLabel);

        // �I�𒆂̃t�H���_�ꗗ�\��
        if (selectedFolders.Count > 0)
        {
            for (int i = 0; i < selectedFolders.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(selectedFolders[i]);
                if (GUILayout.Button("�폜", GUILayout.Width(60)))
                {
                    selectedFolders.RemoveAt(i);
                    i--;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("�t�H���_���I������Ă��܂���B");
        }

        // �t�H���_�ǉ��{�^��
        if (GUILayout.Button("�t�H���_��ǉ�"))
        {
            string folderPath = EditorUtility.OpenFolderPanel("�t�H���_��I��", Application.dataPath, "");
            if (!string.IsNullOrEmpty(folderPath))
            {
                if (folderPath.StartsWith(Application.dataPath))
                {
                    string relativePath = "Assets" + folderPath.Substring(Application.dataPath.Length);
                    if (!selectedFolders.Contains(relativePath))
                    {
                        selectedFolders.Add(relativePath);
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("�G���[", "Assets�t�H���_���̃p�X��I�����Ă��������B", "OK");
                }
            }
        }

        EditorGUILayout.Space();

        // ���g�p�A�Z�b�g�̃X�L�������s
        if (GUILayout.Button("���g�p�A�Z�b�g���X�L����"))
        {
            ScanUnusedAssets();
            scanPerformed = true;
        }

        EditorGUILayout.Space();

        // �X�L���������s����Ă���ꍇ
        if (scanPerformed)
        {
            // �S�č폜�{�^�����㕔�ɔz�u�i��ɕ\���j
            if (GUILayout.Button("�S�č폜"))
            {
                if (unusedAssets.Count == 0)
                {
                    EditorUtility.DisplayDialog("���", "�폜����A�Z�b�g�͂���܂���B", "OK");
                }
                else if (EditorUtility.DisplayDialog("�m�F", "�{���ɍ폜���܂����H", "�͂�", "������"))
                {
                    DeleteUnusedAssets();
                }
            }

            EditorGUILayout.Space();

            // �X�N���[���r���[�Ŗ��g�p�A�Z�b�g�̈ꗗ��\��
            EditorGUILayout.LabelField("���g�p�Ɣ��肳�ꂽ�A�Z�b�g�F" + unusedAssets.Count + "��");
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(300));
            foreach (string asset in unusedAssets)
            {
                EditorGUILayout.LabelField(asset);
            }
            EditorGUILayout.EndScrollView();
        }
    }

    // ���g�p�A�Z�b�g�̃X�L��������
    private void ScanUnusedAssets()
    {
        unusedAssets.Clear();

        // �I�������t�H���_���̑S�A�Z�b�g�������W
        List<string> candidateAssetPaths = new List<string>();
        foreach (string folderPath in selectedFolders)
        {
            string[] guids = AssetDatabase.FindAssets("", new[] { folderPath });
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                // �t�H���_�͑ΏۊO
                if (!AssetDatabase.IsValidFolder(assetPath))
                {
                    candidateAssetPaths.Add(assetPath);
                }
            }
        }

        // �v���W�F�N�g���̑S�V�[���̈ˑ��A�Z�b�g���擾
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });
        HashSet<string> usedAssets = new HashSet<string>();
        foreach (string guid in sceneGuids)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(guid);
            string[] dependencies = AssetDatabase.GetDependencies(scenePath, true);
            foreach (string dependency in dependencies)
            {
                usedAssets.Add(dependency);
            }
        }

        // ���g�p�A�Z�b�g�𒊏o
        foreach (string assetPath in candidateAssetPaths)
        {
            if (!usedAssets.Contains(assetPath))
            {
                unusedAssets.Add(assetPath);
            }
        }

        EditorUtility.DisplayDialog("�X�L��������",
            $"�Ώۃt�H���_�� {candidateAssetPaths.Count} ���̃A�Z�b�g�̂����A{unusedAssets.Count} �������g�p�Ɣ��肳��܂����B",
            "OK");
    }

    // ���g�p�A�Z�b�g�̍폜����
    private void DeleteUnusedAssets()
    {
        int deleteCount = 0;
        foreach (string assetPath in unusedAssets)
        {
            if (AssetDatabase.DeleteAsset(assetPath))
            {
                deleteCount++;
            }
        }
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("�폜����", $"{deleteCount} ���̃A�Z�b�g���폜���܂����B", "OK");
        // �폜��Ƀ��X�g���N���A
        unusedAssets.Clear();
    }
}