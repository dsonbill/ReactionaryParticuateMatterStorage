using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulatorLabel : MonoBehaviour
{
    public Text text;

    public ManipulatorRing A;
    public ManipulatorRing B;
    public ManipulatorRing C;
    public ManipulatorRing D;
    public ManipulatorRing Mode;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "#~";
    }

    void OnValueChanged()
    {
        double val = 0;
        val += A.Value * 100;
        val += B.Value * 10;
        val += C.Value;
        val *= D.Value;

        string textValue = "";

        switch(Mode.Value)
        {
            case (int)ManipulatorRing.Mode.Add:
                textValue = "+";
                break;
            case (int)ManipulatorRing.Mode.Subtract:
                textValue = "-";
                break;
            case (int)ManipulatorRing.Mode.Multiply:
                textValue = "*";
                break;
            case (int)ManipulatorRing.Mode.Divide:
                textValue = "/";
                break;
            case (int)ManipulatorRing.Mode.Exponent:
                textValue = "^";
                break;
        }

        if (val > 1000000000)
        {
            textValue = textValue + Math.Round((val / 1000000000), 3) + " G";
        }
        else if (val > 1000000)
        {
            textValue = textValue + Math.Round((val / 1000000), 3) + " M";
        }
        else if (val > 1000)
        {
            textValue = textValue + Math.Round((val / 1000), 3) + " k";
        }
        else
        {
            textValue = textValue + Math.Round(val, 3);
        }
        text.text = textValue;
    }

    // Better late than never
    void Update()
    {
        A.onValueChanged += OnValueChanged;
        B.onValueChanged += OnValueChanged;
        C.onValueChanged += OnValueChanged;
        D.onValueChanged += OnValueChanged;
        Mode.onValueChanged += OnValueChanged;
        enabled = false;
    }
}
