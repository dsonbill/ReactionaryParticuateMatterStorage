using System;
using System.Collections.Generic;
using UnityEngine;


// Written by Adia Mott and The False Spy
// The most important part of the research being done in this program.

public class VariableWaveFunction : MonoBehaviour
{
    //Pressure and density, horribly wrong
    public Graphing Graph;

    public CollapsingDisk.BasicStructure BasicStructure;

    // Sin Tag 01 --> cos(90° − A)
    public double FutureAmplitude;
    public double FutureAmplitudeDistance;

    public double FiniteAmplitude; // Static wave amplitude
    public double PeakAmplitudeEffect; // Maximum wave amplitude variation

    public double SpacialAdherence { get { return 90 - WellDegree(); } } // How well the VWF adheres to spacetime structure

    public double PeakMomentum { get { return period * FiniteAmplitude * SpacialAdherence; } } // VWF does not run at a constant speed, and so must have a falling momentum

    public double AbsoluteClock; // Time since VWF began oscilating
    public double RelativeInternalClock { get { return TimeDisplacement() / AbsoluteClock; } } // Clock relative to internal IVDPW time

    public double MaximumSpeed { get { return PeakMomentum / (period * SpacialAdherence); } }


    public Func<double> TimeDisplacement;

    public Func<double> WellDegree;


    public double Sample { get { return VariableFunction(period) * period / SpacialAdherence * (FutureAmplitude / FutureAmplitudeDistance) * SpacialAdherence; } }


    double period;
    bool periodDirection;

    // Start is called before the first frame update
    void Start()
    {
        WellDegree = () => { return BasicStructure.Degree; };

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AbsoluteClock += Time.deltaTime;

        if (!periodDirection)
        {
            period += Time.deltaTime;
        }
        else
        {
            period -= Time.deltaTime;
        }

        if (period >= 1)
        {
            periodDirection = true;
        }
        else if (period <= -1)
        {
            periodDirection = false;
        }

        Graph.AddValue((float)Sample);
    }

    double VariableFunction(double time)
    {
        double Y = Math.Asin((FiniteAmplitude/Math.Sin(WellDegree())) / PeakAmplitudeEffect);
        double a = 180 - WellDegree() - Y;

        double i = (Math.Sin(a) * PeakAmplitudeEffect) / Math.Sin(WellDegree());

        return i * time;
    }
}
