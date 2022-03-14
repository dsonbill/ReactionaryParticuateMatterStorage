using UnityEngine.UI.Extensions;
using System;
using UnityEngine;


public class Graphing : MonoBehaviour
{
    public UILineTextureRenderer LineTextureRenderer;
    private float[] values;

    public Color LineColor = Color.green;

    bool dirty = false;

    void Start()
    {
        values = new float[32];
        for (int i = 0; i < 32; i++)
        {
            values[i] = 0f;
        }
        LineTextureRenderer.color = LineColor;
    }

    void Update()
    {
        if (dirty)
        {
            LineTextureRenderer.SetVerticesDirty();
            dirty = false;
        }
    }

    public void AddValue(float value)
    {
        float x = value;
        if (float.IsNaN(value) || float.IsInfinity(value))
        {
            x = 0;
        }
        ShiftList(x);
        DrawGraph();
    }

    void ShiftList(float value)
    {
        float[] tmp = new float[32];
        for (int i = 1; i < 32; i++)
        {
            tmp[i - 1] = values[i];
        }
        tmp[31] = value;
        values = tmp;
    }


    void DrawGraph()
    {
        for (int i = 0; i < 32; i++)
        {
            LineTextureRenderer.Points[i].y = values[i];
        }
        dirty = true;
    }
}