using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InterfaceTypeAttribute))]
public class InterfaceTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        InterfaceTypeAttribute interfaceTypeAttribute = (InterfaceTypeAttribute)attribute;
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(UnityEngine.Object), true);

            if (property.objectReferenceValue != null && !interfaceTypeAttribute.requiredType.IsInstanceOfType(property.objectReferenceValue))
            {
                property.objectReferenceValue = null;
                Debug.LogWarning($"Assigned object must implement {interfaceTypeAttribute.requiredType.Name}");
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use InterfaceType with Object Reference.");
        }

        EditorGUI.EndProperty();
    }
}