using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(InterfaceAttribute))]
public class InterfacePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        InterfaceAttribute interfaceAttribute = (InterfaceAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        var obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(UnityEngine.Object), true);

        if (obj == null || (obj is GameObject gameObject && interfaceAttribute.InterfaceType.IsAssignableFrom(gameObject.GetComponent(interfaceAttribute.InterfaceType)?.GetType())))
        {
            property.objectReferenceValue = obj;
        }
        else
        {
            property.objectReferenceValue = null;
        }

        EditorGUI.EndProperty();
    }
}