using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralSpace : MonoBehaviour
{
    public Material Expansion;
    public Material Contraction;
    public MeshRenderer Renderer;
    public float ContractiveSpace;
    public float ExpansiveSpace;

    public double EnergyLevel;

    float phaseOffset;

    public Yellow Yellow;
    public Purple Purple;

    void Start()
    {
        phaseOffset = (float)RNG.Random.NextDouble();
    }

    void FixedUpdate()
    {
        double energy = GetWave();
        if (energy > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, ContractiveSpace, transform.localPosition.z);
            Renderer.material = Contraction;
            Yellow.Energy = energy;
            Purple.Energy = 0;
        }
        else if (energy < 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, ExpansiveSpace, transform.localPosition.z);
            Renderer.material = Expansion;
            Yellow.Energy = 0;
            Purple.Energy = energy;
        }
        else transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }

    public double GetWave()
    {
        return 0.5 * Math.Sin(2 * Math.PI * EnergyLevel * Time.realtimeSinceStartup + (phaseOffset * EnergyLevel));
    }
}
