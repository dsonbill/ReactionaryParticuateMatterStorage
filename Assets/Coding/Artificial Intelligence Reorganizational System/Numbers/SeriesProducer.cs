using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesProducer : MonoBehaviour
{

    public Graphing graphing;

    float max;
    float time;

    public float Plot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //graphing.AddValue((float)NormalSineWave(1000, 100, 300));
        //graphing.AddValue((float)Resistance()* 10);
        //graphing.AddValue((float)NormalSineWave(1000, 100, 250) / (float)VariableWave(1, 7, 2));
        //graphing.AddValue((float)VariableWave(1, 7, 2));
        //graphing.AddValue((float)Reduction(14, 200));


        time += Time.deltaTime;

        if (time > 10)
        {
            time = 0;
        }


        ChaoticOrganizer.Payload pld = ChaoticOrganizer.Instance.Pump();
        float data = (((float)Kick(pld.Code, pld.Link, pld.Information) / 1000)
            / ((float)Reduction(pld.Link, pld.Information) / 10000)
            * ((float)VariableWave(pld.Information, pld.Link, pld.Code) / 100000)
            + ((float)NormalSineWave(pld.Code, pld.Link, pld.Information) / 1000000))
            ;// 10000000;

        if (max < data)
        {
            max = data;
        }

        

        float plot = ScaleBetweenNumber(data, 0, max, 0, 200);

        graphing.AddValue(plot);
        Plot = plot;
    }

    float ScaleBetweenNumber(float measurement, float measMin, float measMax, float scaleMin, float scaleMax)
    {
        return ((measurement - measMin) / (measMax - measMin)) * (scaleMax - scaleMin) + scaleMin;
    }

    double Resistance()
    {
        //Backpressure
        return 0;
    }

    float Displacement()
    {
        //How to stay afloat
        return 0;
    }

    float Mirror()
    {
        //What the other side sees
        return 0;
    }

    float Release()
    {
        //equal on both sides
        //Built Linearly
        //Equal Distance
        //
        return 0;
    }

    double NormalSineWave(double freq, double rate, int vol)
    {
        //Repressurization

        double theta = freq * (2 * Math.PI) / rate;
        // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
        // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)

        return ((vol >> 2) * Math.Sin(theta * time)) + (vol / 4);
    }

    double VariableWave(double x, double y, double z)
    {
        return Math.Pow(Math.Abs((time % x) - y), z);
    }

    double Reduction(double amplitude, double freq)
    {
        return Math.Abs((time % amplitude) - (amplitude / 2));
    }

    double Kick(double freq, double x, double y)
    {
        return ((time * freq) % x) < Time.realtimeSinceStartup ? y : 0;
    }

    //Kick / Reduction * VariableWave + Normal
}
