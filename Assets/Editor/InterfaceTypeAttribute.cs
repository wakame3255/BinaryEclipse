using UnityEngine;

public class InterfaceTypeAttribute : PropertyAttribute
{
    public System.Type requiredType;

    public InterfaceTypeAttribute(System.Type type)
    {
        requiredType = type;
    }
}