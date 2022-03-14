using UnityEngine;
using System;
using System.Collections.Generic;

public class CurvedLineRenderer : MonoBehaviour {
    public Material lineMat;
    LineRenderer lineRenderer;
    public float wireWidth = 0.01f;
    public List<Transform> Targets;
    public float Smoothness;

    Vector3[] points;

    // Use this for initialization
    void Start ()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMat;
        //lineRenderer.SetColors(c1, c2);
        lineRenderer.startWidth = wireWidth;
        lineRenderer.endWidth = wireWidth;
    }

    void FixedUpdate()
    {
        points = Curver.MakeSmoothCurve(Targets.ToArray(), Smoothness);

        lineRenderer.positionCount = points.Length;

        int counter = 0;
        foreach (Vector3 i in points)
        {
            lineRenderer.SetPosition(counter, i);
            ++counter;
        }
    }
}
