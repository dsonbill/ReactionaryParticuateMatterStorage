using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shells : MonoBehaviour
{
    // How to control the sun in the mirror universe.
    // Careful now
    public SuperParticle SuperParticle;
    public GameObject Vacuum;
    public GameObject Expansion;

    public double VacuumEnergyLevel;
    public double ExpansionEnergyLevel;

    public Red Red;
    public Black Black;

    Vector3 VacuumScale;
    Vector3 ExpansionScale;

    // Start is called before the first frame update
    void Start()
    {
        VacuumScale = Vacuum.transform.localScale;
        ExpansionScale = Expansion.transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        double redEnergyLevel = 0;
        foreach (Red red in FieldMaster.Red)
        {
            if (red == Red) continue;
            redEnergyLevel -= red.Energy / Vector3.Distance(Red.transform.position, red.transform.position);
        }

        VacuumEnergyLevel = -(SuperParticle.NegativeEnergyLevel + SuperParticle.PositiveEnergyLevel) * redEnergyLevel;
        ExpansionEnergyLevel = (SuperParticle.NegativeEnergyLevel - SuperParticle.PositiveEnergyLevel) * redEnergyLevel;

        Red.Energy = VacuumEnergyLevel;
        Black.Energy = ExpansionEnergyLevel;

        Vector3 scale = VacuumScale * (float)VacuumEnergyLevel;
        if (float.IsNaN(scale.x) || float.IsNaN(scale.y) || float.IsNaN(scale.z))
        {
            return;
        }

        if (float.IsInfinity(scale.x) || float.IsInfinity(scale.y) || float.IsInfinity(scale.z))
        {
            return;
        }
        Vacuum.transform.localScale = scale;

        scale = ExpansionScale * (float)ExpansionEnergyLevel;
        if (float.IsNaN(scale.x) || float.IsNaN(scale.y) || float.IsNaN(scale.z))
        {
            return;
        }

        if (float.IsInfinity(scale.x) || float.IsInfinity(scale.y) || float.IsInfinity(scale.z))
        {
            return;
        }
        Expansion.transform.localScale = scale;
    }
}
