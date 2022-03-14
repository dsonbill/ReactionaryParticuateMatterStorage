using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PositiveWellPiston : MonoBehaviour
{
    public double EnergyLevel;
    public Red Red;

    float phaseOffset;

    void Start()
    {
        phaseOffset = (float)RNG.Random.NextDouble();
    }

    public void UpdateEnergy(double energy)
    {
        Red.Energy = energy;
        transform.localPosition = new Vector3(transform.localPosition.x, (float)energy, transform.localPosition.z);
    }

    public double GetWave()
    {
        return 0.5 * Math.Sin(2 * Math.PI * EnergyLevel * Time.realtimeSinceStartup + (phaseOffset * EnergyLevel));
    }

    void FixedUpdate()
    {
        UpdateEnergy(GetWave());
    }
}
