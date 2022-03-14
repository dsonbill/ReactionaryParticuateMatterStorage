using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMaster : MonoBehaviour
{
    public static List<Red> Red = new List<Red>();
    public static List<Black> Black = new List<Black>();
    public static List<Yellow> Yellow = new List<Yellow>();
    public static List<Purple> Purple = new List<Purple>();


    private void FixedUpdate()
    {
        foreach (Field field in Red)
        {
            field.FUpdate();
        }
        foreach (Field field in Black)
        {
            field.FUpdate();
        }
        foreach (Field field in Yellow)
        {
            field.FUpdate();
        }
        foreach (Field field in Purple)
        {
            field.FUpdate();
        }
    }
}
