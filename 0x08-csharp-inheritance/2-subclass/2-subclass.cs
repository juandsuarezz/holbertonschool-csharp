using System;

/// <summary>Class for methods on objects</summary>
class Obj
{
    /// <summary>Checks if a class is a subclass of another class</summary>
    public static bool IsOnlyASubclass(Type derivedType, Type baseType)
    {
        return (derivedType.IsSubclassOf(baseType));
    }
}