using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Transmission;

public class Bus : MonoBehaviour
{
    public Transmitter Selector;
    public Transmitter Output;

    public List<Transmitter> Inputs = new List<Transmitter>();

    public Text Selection;

    void Start()
    {
        Output.manipulator = Manipulate;
    }

    void Update()
    {
        Selection.text = "Selection: " + ((int)Selector.rx).ToString();
    }

    double Manipulate(double data)
    {
        if ((int)Selector.rx > Inputs.Count)
        {
            return 0;
        }
        else if ((int)Selector.rx < 0)
        {
            return 0;
        }

        return Inputs[(int)Selector.rx].rx;
    }
}
