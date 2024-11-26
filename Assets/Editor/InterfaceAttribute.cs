using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class InterfaceAttribute : PropertyAttribute
{
    public Type InterfaceType { get; private set; }

    public InterfaceAttribute(Type interfaceType)
    {
        InterfaceType = interfaceType;
    }
}