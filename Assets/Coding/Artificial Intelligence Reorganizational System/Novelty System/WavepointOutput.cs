using Transmission;
using UnityEngine;

public class WavepointOutput : MonoBehaviour
{
    public Transmitter Transmitter;
    public WavepointGenerator WavepointGenerator;

    void Update()
    {
        Transmitter.manipulator = Manipulate;
    }

    double Manipulate(double data)
    {
        return WavepointGenerator.latestNovelty;
    }
}
