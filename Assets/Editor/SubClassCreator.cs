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
    private string[] abstractClassNames; // �v���W�F�N�g����abstract�N���X�����X�g
    private int selectedClassIndex = 0;  // �I�𒆂�abstract�N���X�C���f�b�N�X
    private string newScriptName = "NewScript"; // �V�����X�N���v�g�̖��O

    [MenuItem("Tools/Abstract Class Generator")]
    public static void ShowWindow()
    {
        GetWindow<SubClassCreater>("Sub Class Generator");
    }

    private void OnEnable()
    {
        // abstract�N���X�����[�h
        LoadAbstractClasses();
    }

    private void LoadAbstractClasses()
    {
        // Assembly-CSharp�Ɍ��肵��abstract�N���X���擾
        Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");

        if (assembly == null)
        {
            Debug.LogWarning("Assembly-CSharp��������܂���ł����B");
            abstractClassNames = new string[0];
            return;
        }
        abstractClassNames = assembly.GetTypes()
                 .Where(type => type.IsClass && type.IsAbstract && type.IsVisible) // abstract�N���X�̂�
                 .Select(type => type.FullName)
                 .ToArray();
    }

    private void OnGUI()
    {
        // �X�N���v�g���̓���
        newScriptName = EditorGUILayout.TextField("Script Name", newScriptName);

        // abstract�N���X�̑I���h���b�v�_�E��
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
            EditorGUILayout.LabelField("�v���W�F�N�g���Ɋ��N���X��������܂���ł����B");
        }

        if (GUILayout.Button("Reload"))
        {
            LoadAbstractClasses();
        }
    }

    private void CreateScriptFromAbstractClass(string baseClassName, string scriptName)
    {
        // �V�����X�N���v�g�̓��e���쐬
        string scriptContent = $"using System;\n\n" +//2�i���s
                               $"public class {scriptName} : {baseClassName}\n" +
                               "{\n" +
                               "\n" +
                               "}";

        // �f�t�H���g�ۑ���p�X������
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

        // �X�N���v�g���t�@�C���ɏ�������
        File.WriteAllText(scriptPath, scriptContent);
        Debug.Log("�X�N���v�g����������܂���: " + scriptPath);

        // �A�Z�b�g�f�[�^�x�[�X���X�V
        AssetDatabase.Refresh();
    }
}
#endif