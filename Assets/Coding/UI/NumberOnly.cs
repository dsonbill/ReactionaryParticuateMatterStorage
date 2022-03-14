using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOnly : MonoBehaviour
{
    public Text text;
    public string value = "";
    public void OnValueChanged(string input)
    {

        if (!int.TryParse(input, out int num))
        {
            text.text = value;
        }
        else if (!float.TryParse(input, out float fnum))
        {
            text.text = value;
        }
        else
        {
            value = input;
        }

    }
}
