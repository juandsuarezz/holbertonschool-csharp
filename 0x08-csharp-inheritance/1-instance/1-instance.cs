using System;

/// <summary>Class to conatin object methods</summary>
class Obj
{
    /// <summary>Gets if an object is an instance of Array</summary>
    public static bool IsInstanceOfArray(object obj)
    {
        return (obj is Array);
    }
}