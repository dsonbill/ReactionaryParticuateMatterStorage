using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    public static double Normalized(this double x1, double minX, double maxX)
    {
        return (x1 - minX) / (maxX - minX);
    }
}
