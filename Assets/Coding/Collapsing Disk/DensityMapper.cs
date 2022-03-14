using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mysterious Woman Complex

public class DensityMapper : MonoBehaviour
{
    //Creates a vacuum / potential well that collects variable wave function information, to be processed later

    public CollapsingDisk.BasicStructure BasicStructure;

    public double MapperDensity { get { return BasicStructure.Density * BasicStructure.ITDRatio; } }

    public List<Color> Colors;

    public Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    public Color Sample;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        gradient = new Gradient();
        colorKey = new GradientColorKey[Colors.Count];
        alphaKey = new GradientAlphaKey[Colors.Count];

        for (int i = 0; i < Colors.Count; i++)
        {
            colorKey[i].color = Colors[i];
            colorKey[i].time = 1 / Colors.Count * i;
        }

        float densityRatio = (float)MapperDensity / Colors.Count;
        for (int i = 0; i < Colors.Count; i++)
        {
            alphaKey[i].alpha = 1.0f / Colors.Count * i * (densityRatio * i);
            alphaKey[i].time = 1 / Colors.Count * i;
        }

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            time = 0;
        }

        Sample = gradient.Evaluate(time);
        //Debug.Log(Sample);
    }
}
