using System;
using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    public static T[] ShiftRight<T>(this T[] array)
    {
        var destination = new T[array.Length];
        Array.Copy(array, 0, destination, 1, array.Length - 1);
        return destination;
    }

    public static T[] ShiftLeft<T>(this T[] array)
    {
        var destination = new T[array.Length];
        Array.Copy(array, 1, destination, 0, array.Length - 1);
        return destination;
    }
}
