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

    [MenuItem("Tools/未使用アセットクリーナー")]
    static void ShowWindow()
    {
        GetWindow<UnusedAssetsCleaner>("未使用アセットクリーナー");
    }

    private void OnGUI()
    {
        GUILayout.Label("未使用アセットクリーナー", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // フォルダ選択部分
        GUILayout.Label("検索対象フォルダ:");

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));

        for (int i = 0; i < selectedFolders.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(selectedFolders[i]);
            if (GUILayout.Button("削除", GUILayout.Width(60)))
            {
                selectedFolders.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("フォルダを追加"))
        {
            string folder = EditorUtility.OpenFolderPanel("検索するフォルダを選択", "Assets", "");
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

        if (GUILayout.Button("未使用アセットを検索"))
        {
            FindUnusedAssets();
        }

        // 未使用アセット表示部分
        if (unusedAssets.Count > 0)
        {
            EditorGUILayout.Space();
            GUILayout.Label($"未使用アセット ({unusedAssets.Count}個):");

            EditorGUILayout.BeginScrollView(Vector2.zero);
            foreach (string asset in unusedAssets)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(asset);
                if (GUILayout.Button("選択", GUILayout.Width(60)))
                {
                    Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(asset);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            if (!showConfirmation)
            {
                if (GUILayout.Button("選択した未使用アセットを削除"))
                {
                    showConfirmation = true;
                }
            }
            else
            {
                EditorGUILayout.HelpBox("本当に削除しますか？この操作は取り消せません。", MessageType.Warning);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("はい、削除します"))
                {
                    DeleteUnusedAssets();
                    showConfirmation = false;
                }
                if (GUILayout.Button("キャンセル"))
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
            EditorUtility.DisplayDialog("エラー", "フォルダを選択してください。", "OK");
            return;
        }

        unusedAssets.Clear();

        // すべての依存関係を取得
        string[] allDependencies = AssetDatabase.GetAllAssetPaths()
            .SelectMany(path => AssetDatabase.GetDependencies(path, true))
            .Distinct()
            .ToArray();

        // 選択されたフォルダ内のアセットをチェック
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
            EditorUtility.DisplayDialog("結果", "未使用のアセットは見つかりませんでした。", "OK");
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
        EditorUtility.DisplayDialog("完了", "選択された未使用アセットを削除しました。", "OK");
    }
}