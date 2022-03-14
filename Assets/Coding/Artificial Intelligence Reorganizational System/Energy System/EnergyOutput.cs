using Transmission;
using UnityEngine;

public class EnergyOutput : MonoBehaviour
{
    public Transmitter Transmitter;
    public EnergyResponseSystem ERS;

    void Start()
    {
        Transmitter.manipulator = Manipulate;
    }

    double Manipulate(double data)
    {
        return ERS.EnergyLevel;
    }
}