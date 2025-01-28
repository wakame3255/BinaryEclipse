using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


#if UNITY_EDITOR
using UnityEditor;

public class SubClassGenerator : EditorWindow
{
    private string[] _baseClassOptions;
    private string[] _interfaceOptions;
    private int _selectedClassIndex;
    private int _selectedMask;
    private string _newScriptName = "NewClass";

    [MenuItem("Tools/Sub Class Generator")]
    public static void ShowWindow()
    {
        GetWindow<SubClassGenerator>("Sub Class Generator");
    }

    private void OnEnable()
    {
        LoadAbstractClasses();
        LoadInterfaces();
    }

    private void LoadAbstractClasses()
    {
        Assembly assembly = GetAssemblyCS();

        if (assembly == null)
        {
            Debug.LogWarning("Assembly-CSharpが見つかりませんでした。");
            _baseClassOptions = new[] { "None" };
            return;
        }

        _baseClassOptions = assembly.GetTypes()
            .Where(type => type.IsClass && type.IsAbstract && type.IsVisible)
            .Select(type => type.FullName)
            .Prepend("None")
            .ToArray();
    }

    private void LoadInterfaces()
    {
        Assembly assembly = GetAssemblyCS();

        if (assembly == null)
        {
            Debug.LogWarning("Assembly-CSharpが見つかりませんでした。");
            _interfaceOptions = Array.Empty<string>();
            return;
        }

        _interfaceOptions = assembly.GetTypes()
            .Where(type => type.IsInterface)
            .Select(type => type.FullName)
            .ToArray();

        _selectedMask = 0;
    }

    private void OnGUI()
    {
        _newScriptName = EditorGUILayout.TextField("Script Name", _newScriptName);

        if (_baseClassOptions.Length > 0)
        {
            _selectedClassIndex = EditorGUILayout.Popup("Select Base Class", _selectedClassIndex, _baseClassOptions);
        }
        else
        {
            EditorGUILayout.LabelField("プロジェクト内に基底クラスが見つかりませんでした。");
        }

        if (_interfaceOptions.Length > 0)
        {
            _selectedMask = EditorGUILayout.MaskField("Select Interface", _selectedMask, _interfaceOptions);
        }
        else
        {
            EditorGUILayout.LabelField("プロジェクト内にインターフェースが見つかりませんでした。");
        }

        if (GUILayout.Button("Create Script"))
        {
            CreateScript(_baseClassOptions[_selectedClassIndex], _interfaceOptions, _newScriptName);
        }

        if (GUILayout.Button("Reload Classes"))
        {
            LoadAbstractClasses();
            LoadInterfaces();
        }
    }

    private void CreateScript(string baseClassName, string[] interfaceNames, string scriptName)
    {
        var selectedInterfaces = interfaceNames
            .Where((_, index) => (_selectedMask & (1 << index)) != 0)
            .ToList();

        var inheritanceList = new List<string>();

        if (baseClassName != "None")
        {
            inheritanceList.Add(baseClassName);
        }
        inheritanceList.AddRange(selectedInterfaces);
        string inheritance = string.Empty;
        if (inheritanceList.Count > 0)
        {
            inheritance = $" : {string.Join(", ", inheritanceList)}";
        }


        string methodStubs = string.Empty;

        if (baseClassName != "None")
        {
            Assembly assembly = GetAssemblyCS();

            Type baseClassType = assembly?.GetType(baseClassName);

            if (baseClassType != null)
            {
                IEnumerable<string> abstractMethods = baseClassType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(mInfo => mInfo.IsAbstract)
                .Select(mInfo => $"    public override {GetReturnType(mInfo)} {mInfo.Name}({string.Join(", ", mInfo.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))})\n    {{\n        throw new NotImplementedException();\n    }}\n");

                methodStubs += string.Join("\n", abstractMethods);
            }
        }

        foreach (string interfaceName in selectedInterfaces)
        {
            Assembly assembly = GetAssemblyCS();

            Type interfaceType = assembly?.GetType(interfaceName);

            if (interfaceType != null)
            {
                IEnumerable<string> interfaceMethods = interfaceType.GetMethods()
                  .Select(m => $"    public {GetReturnType(m)} {m.Name}({string.Join(", ", m.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))})\n    {{\n        throw new NotImplementedException();\n    }}\n");

                methodStubs += string.Join("\n", interfaceMethods);
            }
        }

        string scriptContent = $"using System;\n\n" +
                               $"public class {scriptName}{inheritance}\n" +
                               "{\n" +
                               methodStubs +
                               "}";

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

        File.WriteAllText(scriptPath, scriptContent);
        Debug.Log("スクリプトが生成されました: " + scriptPath);

        AssetDatabase.Refresh();
    }

   private string GetReturnType(MethodInfo info)
    {
        
        if(info.ReturnType==typeof(void))
        {
            return "void";
        }
        if(info.ReturnType==typeof(System.Object))
        {
            return "object";
        }
        if(info.ReturnType==typeof(UnityEngine.Object))
        {
            return "UnityEngine.Object";
        }
        return info.ReturnType.Name;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Assembly GetAssemblyCS()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");
    }
}
#endif