using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Lucifer & Alterous

public class Space : MonoBehaviour
{
    static public Space DynamicSpace;
    public MSTCP MSTCP;

    public enum Symbol
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public List<double[]> Available = new List<double[]>();

    bool Expanding = true;
    public int Availability;

    System.Random r = new System.Random();

    float tick = 0f;
    int tock = 0;

    //Fucking fields maine
    //Energy System
    //Release State
    //Program Later

    // Start is called before the first frame update

    //12 17 2021
    void Start()
    {
        Available.Add(new double[3] { double.MaxValue, double.MaxValue, double.MaxValue });
        DynamicSpace = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Expanding)
        {
            List<double[]> newEntries = new List<double[]>();
            for (int i = tock; i < Available.Count; i++)
            {
                if (tick < 1f)
                {
                    tick += Time.deltaTime;
                    break;
                }
                tick = 0f;

                double[] x = new double[3]
                {
                    DoProcess(Available[i][0], GetSymbol()) * MSTCP.Contactless((float)Available[i][0]) / 45129,
                    DoProcess(Available[i][1], GetSymbol()) * MSTCP.Contactless((float)Available[i][1]) / 45129,
                    DoProcess(Available[i][2], GetSymbol()) * MSTCP.Contactless((float)Available[i][2]) / 45129
                };
                //Debug.Log(x[0] + " " + x[1] + " " + x[2]);

                Available[i] = new double[3]
                {
                    Available[i][0] - x[0],
                    Available[i][1] - x[1],
                    Available[i][2] - x[2]
                };

                newEntries.Add(new double[3]
                {
                    MSTCP.Exact((float)x[0] * 120.0f, 180.0f),
                    MSTCP.Exact((float)x[1] * 120.0f, 180.0f),
                    MSTCP.Exact((float)x[2] * 120.0f, 180.0f)
                });
            }

            int y = Available.Count;

            foreach (double[] z in newEntries)
            {
                double[] arr = new double[3]
                {
                    MSTCP.Pr((float)z[0] - 1.0f) - 314159,
                    MSTCP.Pr((float)z[1] - 1.0f) - 314159,
                    MSTCP.Pr((float)z[2] - 1.0f) - 314159
                };

                //874003234816

                double x = 0;
                for (int i = 0; i < 3; i++)
                {
                    x += arr[i];
                }

                //1/8th of a degree of a clock's facial awareness of time
                x *= 0.00416666666666666666666666666667;

                x /= 3;

                //Debug.Log(x);

                Available.Add(arr);

                Debug.Log("Delta Applicator Entry: [" + arr[0] + "," + arr[1] + "," + arr[2] + "]");
            }

            if (tick == 0f)
            {
                tock++;
            }

            if (tock == y)
            {
                tock = 0;
            }

            if (Available.Count > Availability)
            {
                Expanding = false;
                Debug.Log("Universal Availability: " + Available.Count);
            }
        }
    }

    Symbol GetSymbol()
    {
        return (Symbol)r.Next(0, 3);
    }

    double DoProcess(double input, Symbol symbol)
    {
        switch (symbol)
        {
            case Symbol.Add:
                return input + r.Next(0, int.MaxValue / r.Next(0, int.MaxValue / 2));
            case Symbol.Subtract:
                return input - r.Next(0, int.MaxValue / r.Next(0, int.MaxValue / 2));
            case Symbol.Multiply:
                return input * r.Next(0, int.MaxValue / r.Next(0, int.MaxValue / 2));
            case Symbol.Divide:
                return input * r.Next(1, int.MaxValue / r.Next(0, int.MaxValue / 2));
        }
        return 0;
    }
}
