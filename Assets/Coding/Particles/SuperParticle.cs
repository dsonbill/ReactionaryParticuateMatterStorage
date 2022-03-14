using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperParticle : MonoBehaviour
{
    public GameObject Electro;
    public GameObject Positro;

    public double PositiveEnergyLevel;
    public double NegativeEnergyLevel;


    void FixedUpdate()
    {
        Electro.transform.localPosition = new Vector3(Electro.transform.localPosition.x, (float)(1*(PositiveEnergyLevel)), Electro.transform.localPosition.z);
        Positro.transform.localPosition = new Vector3(Positro.transform.localPosition.x, (float)(-1*(NegativeEnergyLevel)), Positro.transform.localPosition.z);
    }
}
