using Transmission;
using UnityEngine;

public class HarmonizerSource : MonoBehaviour
{
    public Transmitter Transmitter;
    public Harmonizer Harmonizer;

    void Start()
    {
        Transmitter.manipulator = Manipulate;
    }

    double Manipulate(double data)
    {
        float x = 0;
        for (int i = 0; i < Harmonizer.Sampler.Length; i++)
        {
            x += Harmonizer.Sampler[i];
        }
        x *= Harmonizer.Sampler.Length;

        return x;
    }
}
