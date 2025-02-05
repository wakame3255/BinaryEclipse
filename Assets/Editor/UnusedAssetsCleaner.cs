using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnusedAssetsCleaner : EditorWindow
{
    // 選択されたフォルダ（Assets以下のパス）
    private List<string> selectedFolders = new List<string>();

    // スキャン結果：使われていないアセットのパス一覧
    private List<string> unusedAssets = new List<string>();

    // スキャンが実行されたかどうか
    private bool scanPerformed = false;

    // スクロールビュー用のスクロール位置
    private Vector2 scrollPos;

    [MenuItem("Tools/Unused Assets Cleaner")]
    public static void ShowWindow()
    {
        GetWindow<UnusedAssetsCleaner>("未使用アセット削除エディタ");
    }

    private void OnGUI()
    {
        // 対象フォルダの選択区分
        EditorGUILayout.LabelField("【対象フォルダ選択】", EditorStyles.boldLabel);

        // 選択中のフォルダ一覧表示
        if (selectedFolders.Count > 0)
        {
            for (int i = 0; i < selectedFolders.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(selectedFolders[i]);
                if (GUILayout.Button("削除", GUILayout.Width(60)))
                {
                    selectedFolders.RemoveAt(i);
                    i--;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("フォルダが選択されていません。");
        }

        // フォルダ追加ボタン
        if (GUILayout.Button("フォルダを追加"))
        {
            string folderPath = EditorUtility.OpenFolderPanel("フォルダを選択", Application.dataPath, "");
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
                    EditorUtility.DisplayDialog("エラー", "Assetsフォルダ内のパスを選択してください。", "OK");
                }
            }
        }

        EditorGUILayout.Space();

        // 未使用アセットのスキャン実行
        if (GUILayout.Button("未使用アセットをスキャン"))
        {
            ScanUnusedAssets();
            scanPerformed = true;
        }

        EditorGUILayout.Space();

        // スキャンが実行されている場合
        if (scanPerformed)
        {
            // 全て削除ボタンを上部に配置（常に表示）
            if (GUILayout.Button("全て削除"))
            {
                if (unusedAssets.Count == 0)
                {
                    EditorUtility.DisplayDialog("情報", "削除するアセットはありません。", "OK");
                }
                else if (EditorUtility.DisplayDialog("確認", "本当に削除しますか？", "はい", "いいえ"))
                {
                    DeleteUnusedAssets();
                }
            }

            EditorGUILayout.Space();

            // スクロールビューで未使用アセットの一覧を表示
            EditorGUILayout.LabelField("未使用と判定されたアセット：" + unusedAssets.Count + "件");
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(300));
            foreach (string asset in unusedAssets)
            {
                EditorGUILayout.LabelField(asset);
            }
            EditorGUILayout.EndScrollView();
        }
    }

    // 未使用アセットのスキャン処理
    private void ScanUnusedAssets()
    {
        unusedAssets.Clear();

        // 選択したフォルダ内の全アセット候補を収集
        List<string> candidateAssetPaths = new List<string>();
        foreach (string folderPath in selectedFolders)
        {
            string[] guids = AssetDatabase.FindAssets("", new[] { folderPath });
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                // フォルダは対象外
                if (!AssetDatabase.IsValidFolder(assetPath))
                {
                    candidateAssetPaths.Add(assetPath);
                }
            }
        }

        // プロジェクト内の全シーンの依存アセットを取得
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

        // 未使用アセットを抽出
        foreach (string assetPath in candidateAssetPaths)
        {
            if (!usedAssets.Contains(assetPath))
            {
                unusedAssets.Add(assetPath);
            }
        }

        EditorUtility.DisplayDialog("スキャン完了",
            $"対象フォルダ内 {candidateAssetPaths.Count} 件のアセットのうち、{unusedAssets.Count} 件が未使用と判定されました。",
            "OK");
    }

    // 未使用アセットの削除処理
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
        EditorUtility.DisplayDialog("削除完了", $"{deleteCount} 件のアセットを削除しました。", "OK");
        // 削除後にリストをクリア
        unusedAssets.Clear();
    }
}