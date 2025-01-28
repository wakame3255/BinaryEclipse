using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;


#if UNITY_EDITOR
using UnityEditor;

public class SubClassCreater : EditorWindow
{
    private string[] abstractClassNames; // プロジェクト内のabstractクラス名リスト
    private int selectedClassIndex = 0;  // 選択中のabstractクラスインデックス
    private string newScriptName = "NewScript"; // 新しいスクリプトの名前

    [MenuItem("Tools/Abstract Class Generator")]
    public static void ShowWindow()
    {
        GetWindow<SubClassCreater>("Sub Class Generator");
    }

    private void OnEnable()
    {
        // abstractクラスをロード
        LoadAbstractClasses();
    }

    private void LoadAbstractClasses()
    {
        // Assembly-CSharpに限定してabstractクラスを取得
        Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");

        if (assembly == null)
        {
            Debug.LogWarning("Assembly-CSharpが見つかりませんでした。");
            abstractClassNames = new string[0];
            return;
        }
        abstractClassNames = assembly.GetTypes()
                 .Where(type => type.IsClass && type.IsAbstract && type.IsVisible) // abstractクラスのみ
                 .Select(type => type.FullName)
                 .ToArray();
    }

    private void OnGUI()
    {
        // スクリプト名の入力
        newScriptName = EditorGUILayout.TextField("Script Name", newScriptName);

        // abstractクラスの選択ドロップダウン
        if (abstractClassNames.Length > 0)
        {
            selectedClassIndex = EditorGUILayout.Popup("Select Base Class", selectedClassIndex, abstractClassNames);

            if (GUILayout.Button("Create Script"))
            {
                CreateScriptFromAbstractClass(abstractClassNames[selectedClassIndex], newScriptName);
            }
        }
        else
        {
            EditorGUILayout.LabelField("プロジェクト内に基底クラスが見つかりませんでした。");
        }

        if (GUILayout.Button("Reload"))
        {
            LoadAbstractClasses();
        }
    }

    private void CreateScriptFromAbstractClass(string baseClassName, string scriptName)
    {
        // 新しいスクリプトの内容を作成
        string scriptContent = $"using System;\n\n" +//2段改行
                               $"public class {scriptName} : {baseClassName}\n" +
                               "{\n" +
                               "\n" +
                               "}";

        // デフォルト保存先パスを決定
        string selectedFolderPath = "Assets";
        Object selected = Selection.activeObject;
        if (selected != null)
        {
            selectedFolderPath = AssetDatabase.GetAssetPath(selected);
            if (!Directory.Exists(selectedFolderPath))
            {
                selectedFolderPath = Path.GetDirectoryName(selectedFolderPath);
            }
        }

        string scriptPath = Path.Combine(selectedFolderPath, $"{scriptName}.cs");

        // スクリプトをファイルに書き込み
        File.WriteAllText(scriptPath, scriptContent);
        Debug.Log("スクリプトが生成されました: " + scriptPath);

        // アセットデータベースを更新
        AssetDatabase.Refresh();
    }
}
#endif