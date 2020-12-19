using System;

/// <summary>Class to contain methods for objects</summary>
class Obj
{
    /// <summary>Checks if an object is an int</summary>
    public static bool IsOfTypeInt(object obj)
    {
        return (obj.GetType() == typeof(int));
    }
}