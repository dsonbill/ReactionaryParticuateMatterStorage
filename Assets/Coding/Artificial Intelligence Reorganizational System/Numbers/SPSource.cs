using Transmission;
using UnityEngine;

public class SPSource : MonoBehaviour
{
    public Transmitter Transmitter;
    public SeriesProducer SeriesProducer;

    void Update()
    {
        Transmitter.manipulator = Manipulate;
    }

    double Manipulate(double data)
    {
        return SeriesProducer.Plot;
    }
}
