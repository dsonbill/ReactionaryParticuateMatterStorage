using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by ZigZag

public class MathematicProcessor : MonoBehaviour
{
    public Guid Token;

    public enum Symbol
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public Symbol symbol;
    public int ID;

    //Create Digital Token Engine
    //Rewrite mathematical processees based on Digital Token
    //Restore Image
    //Reveal the nature of humanity
    //Restore Processes Later

    

    // Start is called before the first frame update
    void Start()
    {
        Token = Guid.NewGuid();

        int first = int.Parse(Token.ToString().Substring(0, 4), System.Globalization.NumberStyles.HexNumber);
        int last = int.Parse(Token.ToString().Substring(Token.ToString().Length - 4, 4), System.Globalization.NumberStyles.HexNumber);

        ID = (int)ScaleBetweenNumber(first + last, 0, 131070, 0, 3);
        if (ID <= (int)Symbol.Divide)
        {
            if (ID <= (int)Symbol.Multiply)
            {
                if (ID <= (int)Symbol.Subtract)
                {
                    if (ID <= (int)Symbol.Add)
                    {
                        symbol = Symbol.Add;
                        return;
                    }

                    symbol = Symbol.Subtract;
                    return;
                }

                symbol = Symbol.Multiply;
                return;
            }

            symbol = Symbol.Divide;
        }
    }

    public float Process(float in0, float in1)
    {
        switch (symbol)
        {
            case Symbol.Add:
                return in0 + in1;
            case Symbol.Subtract:
                return in0 - in1;
            case Symbol.Multiply:
                return in0 * in1;
            case Symbol.Divide:
                return in0 / in1;
        }
        return 0;
    }

    float ScaleBetweenNumber(float measurement, float measMin, float measMax, float scaleMin, float scaleMax)
    {
        return ((measurement - measMin) / (measMax - measMin)) * (scaleMax - scaleMin) + scaleMin;
    }
}
