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

    private HashSet<string> _usings=new();
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
            Debug.LogWarning("Assembly-CSharp��������܂���ł����B");
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
            Debug.LogWarning("Assembly-CSharp��������܂���ł����B");
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
            EditorGUILayout.LabelField("�v���W�F�N�g���Ɋ��N���X��������܂���ł����B");
        }

        if (_interfaceOptions.Length > 0)
        {
            _selectedMask = EditorGUILayout.MaskField("Select Interface", _selectedMask, _interfaceOptions);
        }
        else
        {
            EditorGUILayout.LabelField("�v���W�F�N�g���ɃC���^�[�t�F�[�X��������܂���ł����B");
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
        _usings.Clear();//_unisgs�̏�����
        

        List<string> selectedInterfaces = interfaceNames
           .Where((_, index) => (_selectedMask & (1 << index)) != 0)
           .ToList();

        string inheritance = CreateInheritanceSentence(baseClassName,selectedInterfaces.ToArray());
        Assembly assembly = GetAssemblyCS();

        string propertyStub = string.Empty;

        propertyStub += CreateClassPropertyStub(assembly,baseClassName);
        propertyStub += CreateInterfacePropertyStub(assembly,selectedInterfaces.ToArray());

        string methodStubs = string.Empty;

        methodStubs += CreateClassMethodStubs(assembly,baseClassName);
        methodStubs += CreateInterfaceMethodStubs(assembly,selectedInterfaces.ToArray());

        RegisterClassUsings(assembly,baseClassName);
        RegisterInterfaceUsings(assembly,selectedInterfaces.ToArray());

        string usingStatements=string.Empty;
        usingStatements = CreateUsingStatement(_usings);
        

        string scriptContent = $"using System;\n" +
                               $"{usingStatements}\n"+
                               $"public class {scriptName}{inheritance}\n" +
                               "{\n" +
                               propertyStub +
                               methodStubs +
                               "}";

       string scriptPath=GetGeneratePath(scriptName);

        File.WriteAllText(scriptPath, scriptContent);
        Debug.Log("�X�N���v�g����������܂���: " + scriptPath);

        AssetDatabase.Refresh();
    }

    
    /// <summary>
    /// �p�������̕��͂𐶐�
    /// </summary>
    /// <param name="baseClassName"></param>
    /// <param name="selectedInterfaces"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreateInheritanceSentence(string baseClassName, string[] selectedInterfaces)
    {
       

        List<string> inheritanceList = new List<string>();

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
        return inheritance;
    }

    
    #region CreateMethodStub
    /// <summary>
    /// abstractMethod�̕��͂��o��
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="baseClassName"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreateClassMethodStubs(Assembly assembly,string baseClassName)
    {
        string sentence=string.Empty;
        if (baseClassName != "None")
        {


            Type baseClassType = assembly?.GetType(baseClassName);

            if (baseClassType != null)
            {
                IEnumerable<string> abstractMethods = baseClassType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(mInfo => mInfo.IsAbstract&&!mInfo.IsSpecialName)
                .Select(mInfo =>CreateMethodSentence(mInfo,true));

                sentence += string.Join("\n", abstractMethods);
            }
        }
        return sentence;
    }
    /// <summary>
    /// interface�̃��\�b�h�̕��͂��o��
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="selectedInterfaces"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreateInterfaceMethodStubs(Assembly assembly, string[] selectedInterfaces)
    {
        string sentence=string.Empty;
        foreach (string interfaceName in selectedInterfaces)
        {


            Type interfaceType = assembly?.GetType(interfaceName);

            if (interfaceType == null)
            {
                continue;
            }
            IEnumerable<string> interfaceMethods = interfaceType.GetMethods()
                    .Where(mInfo => !mInfo.IsSpecialName)
                  .Select(mInfo => CreateMethodSentence(mInfo));

            sentence += string.Join("\n", interfaceMethods);
        }
        return sentence;
    }
    #endregion CreateMethodStub
    #region CreatePropertyStub

    /// <summary>
    /// �N���X�̃v���p�e�B���擾
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="baseClassName"></param>
    /// <returns></returns>
    [method:MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreateClassPropertyStub(Assembly assembly,string baseClassName)
    {
        string sentence = string.Empty;
        if (baseClassName == "None")
        {
            return string.Empty;
        }
        Type baseClassType = assembly?.GetType(baseClassName);

        if (baseClassType== null)
        {
            return string.Empty;
        }
        // ���ۃv���p�e�B�̎擾�E����
        IEnumerable<string> abstractProperties = baseClassType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(property => property.GetGetMethod(true)?.IsAbstract == true || property.GetSetMethod(true)?.IsAbstract == true)
            .Select(property =>CreatePropertyStub(property,true));
        sentence += string.Join("\n", abstractProperties);
        return sentence + "\n";
    }

    /// <summary>
    /// interface�̃v���p�e�B���擾
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="selectedInterfaces"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreateInterfacePropertyStub(Assembly assembly, string[] selectedInterfaces)
    {
        string sentence = string.Empty;
        foreach (string interfaceName in selectedInterfaces)
        {


            Type interfaceType = assembly?.GetType(interfaceName);

            if (interfaceType == null)
            {
                continue;  
            }
            IEnumerable<string> interfacePropertyStubs = interfaceType.GetProperties()
                 .Where(property => property.GetGetMethod(true)?.IsAbstract == true || property.GetSetMethod(true)?.IsAbstract == true)
                 .Select(property => CreatePropertyStub(property));
            sentence += string.Join("\n", interfacePropertyStubs);
        }
        return sentence+"\n";
    }
    [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
    private string CreatePropertyStub(PropertyInfo property)
    {
        string getPropertySentence = string.Empty;
        bool hasGetter = property.GetGetMethod(true) != null;
        if (hasGetter)
        {
            getPropertySentence = "get => throw new NotImplementedException(); ";
        }

        string setPropertySentence = string.Empty;
        bool hasSetter = property.GetSetMethod(true) != null;
        if (hasSetter)
        {
            setPropertySentence = "set => throw new NotImplementedException(); ";
        }

        return $"    public {ConvertType(property.PropertyType)} {property.Name} {{ " +
               getPropertySentence +
               setPropertySentence +
               "}";
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreatePropertyStub(PropertyInfo property,bool isOverride)
    {
        if(!isOverride)
        {
            CreatePropertyStub(property);
        }
        string getPropertySentence = string.Empty;
        bool hasGetter = property.GetGetMethod(true) != null;
        if (hasGetter)
        {
            getPropertySentence = "get => throw new NotImplementedException(); ";
        }

        string setPropertySentence = string.Empty;
        bool hasSetter = property.GetSetMethod(true) != null;
        if (hasSetter)
        {
            setPropertySentence = "set => throw new NotImplementedException(); ";
        }

        return $"    public override {ConvertType(property.PropertyType)} {property.Name} {{ " +
               getPropertySentence +
               setPropertySentence +
               "}";
    }
    #endregion CreatePropertyStub
    #region CreateMethodSentence
    /// <summary>
    /// ���\�b�h�̏���Ԃ���
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreateMethodSentence(MethodInfo info)
    {
        return $"    public {ConvertType(info.ReturnType)} {info.Name}({string.Join(", ", info.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))})\n    {{\n        throw new NotImplementedException();\n    }}\n";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string CreateMethodSentence(MethodInfo info,bool isOverride)
    {
        if(!isOverride)
        {
            return CreateMethodSentence(info);
        }
        return $"    public override {ConvertType(info.ReturnType)} {info.Name}({string.Join(", ", info.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))})\n    {{\n        throw new NotImplementedException();\n    }}\n";
    }
    /// <summary>
    /// ����ȕϊ��̕K�v�Ȗ߂�l������ꍇ�ϊ����ĕԂ�
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private string ConvertType(Type type)
    {
        
        if(type==typeof(void))
        {
            return "void";
        }
        if(type==typeof(System.Object))
        {
            return "object";
        }
        if(type==typeof(UnityEngine.Object))
        {
            return "UnityEngine.Object";
        }
        if(type==typeof(System.String))
        {
            return "string";
        }
        if(type==typeof(System.Int32))
        {
            return "int";
        }
        return type.Name;
    }
    #endregion CreateMethodSentence

    /// <summary>
    /// ��������p�X���擾
    /// </summary>
    /// <param name="scriptName"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetGeneratePath(string scriptName)
    {
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
        return scriptPath;
    }

    /// <summary>
    /// Assembly-CSharp��Assembly��Ԃ�
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Assembly GetAssemblyCS()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");
    }

    #region usings
   private void RegisterClassUsings(Assembly assembly,string baseClassName)
    {
        string sentence = string.Empty;
        if (baseClassName == "None")
        {
            return;
        }
        Type baseClassType = assembly?.GetType(baseClassName);
        RegisterUsingSentence(baseClassType);
        if (baseClassType == null)
        {
            return;
        }
        IEnumerable<PropertyInfo> properties = baseClassType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                                        .Where(property => property.GetGetMethod(true)?.IsAbstract == true || property.GetSetMethod(true)?.IsAbstract == true);


        foreach (PropertyInfo property in properties)
        {
            RegisterPropertyUsings(property);
        }
        IEnumerable<MethodInfo> methods = baseClassType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                            .Where(mInfo => mInfo.IsAbstract && !mInfo.IsSpecialName);

        foreach (MethodInfo method in methods)
        {
            RegisterMethodUsings(method);
        }
    }
    private void RegisterInterfaceUsings(Assembly assembly, string[] selectInterfaces)
    {
        foreach(string interfaceName in selectInterfaces)
        {
            Type interfaceType = assembly?.GetType(interfaceName);

            if (interfaceType == null)
            {
                continue;
            }
            foreach(PropertyInfo property in interfaceType.GetProperties())
            {
                RegisterPropertyUsings(property);
            }
            foreach(MethodInfo method in interfaceType.GetMethods())
            {
                RegisterMethodUsings(method);
            }
        }

    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RegisterPropertyUsings(PropertyInfo info)
    {
        RegisterUsingSentence(info.PropertyType);
    }

    private void RegisterMethodUsings(MethodInfo info)
    {
        RegisterUsingSentence(info.ReturnType);
        foreach(ParameterInfo parameter in info.GetParameters())
        {
            RegisterUsingSentence(parameter.ParameterType);
        }
    }

    [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
    private void RegisterUsingSentence(Type type)
    {
        if(type.Namespace==null)
        {
            return;
        }
        if(type.Namespace=="System")
        {
            return;
        }
        _usings.Add($"using {type.Namespace};");
    }
    [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
    private string CreateUsingStatement(HashSet<string> usings)
    {
        string sentence=string.Empty;
        sentence = string.Join("\n",_usings);
        return sentence;
    }
    #endregion
}
#endif