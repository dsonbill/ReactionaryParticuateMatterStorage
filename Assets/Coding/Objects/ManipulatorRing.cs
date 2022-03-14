using System;
using System.Collections.Generic;
using UnityEngine;

public class ManipulatorRing : MonoBehaviour
{
    public enum RingType
    {
        Significant,
        Multiplier,
        ModeChange
    }

    public enum Significant
    {
        black = 0,
        brown = 1,
        red = 2,
        orange = 3,
        yellow = 4,
        green = 5,
        blue = 6,
        violet = 7,
        grey = 8,
        white = 9
    }

    public static class ManipulatorValue
    {
        public const float black = 1;
        public const float brown = 10;
        public const float red = 100;
        public const float orange = 1000;
        public const float yellow = 10000;
        public const float green = 100000;
        public const float blue = 1000000;
        public const float violet = 10000000;
        public const float grey = 100000000;
        public const float white = 1000000000;
        public const float gold = 0.1f;
        public const float silver = 0.01f;
    }

    public enum Mode
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Exponent
    }

    public RingType ringType;
    int currentValue;
    public float Value;

    public GameObject GameObject;

    Material material;

    public delegate void OnValueChanged();
    public OnValueChanged onValueChanged;

    void Start()
    {
        material = GameObject.GetComponent<MeshRenderer>().materials[0];
        if (ringType == RingType.Multiplier)
        {
            Value = 1;
        }
    }


    public void OnPointerClick()
    {
        currentValue++;
        if (ringType == RingType.Significant)
        {
            if (currentValue == (int)Significant.white + 1)
            {
                currentValue = 0;
            }
            switch(currentValue)
            {
                case (int)Significant.black:
                    material.SetColor("_Color", Color.black);
                    Value = (int)Significant.black;
                    break;
                case (int)Significant.brown:
                    material.SetColor("_Color", new Color(0.471f, 0.329f, 0.016f));
                    Value = (int)Significant.brown;
                    break;
                case (int)Significant.red:
                    material.SetColor("_Color", Color.red);
                    Value = (int)Significant.red;
                    break;
                case (int)Significant.orange:
                    material.SetColor("_Color", new Color(1, 0.561f, 0.204f));
                    Value = (int)Significant.orange;
                    break;
                case (int)Significant.yellow:
                    material.SetColor("_Color", Color.yellow);
                    Value = (int)Significant.yellow;
                    break;
                case (int)Significant.green:
                    material.SetColor("_Color", Color.green);
                    Value = (int)Significant.green;
                    break;
                case (int)Significant.blue:
                    material.SetColor("_Color", Color.blue);
                    Value = (int)Significant.blue;
                    break;
                case (int)Significant.violet:
                    material.SetColor("_Color", Color.magenta);
                    Value = (int)Significant.violet;
                    break;
                case (int)Significant.grey:
                    material.SetColor("_Color", Color.gray);
                    Value = (int)Significant.grey;
                    break;
                case (int)Significant.white:
                    material.SetColor("_Color", Color.white);
                    Value = (int)Significant.white;
                    break;
            }
        }
        else if (ringType == RingType.Multiplier)
        {
            if (currentValue == 12)
            {
                currentValue = 0;
            }
            switch(currentValue)
            {
                case 0:
                    material.SetColor("_Color", Color.black);
                    Value = ManipulatorValue.black;
                    break;
                case 1:
                    material.SetColor("_Color", new Color(0.471f, 0.329f, 0.016f));
                    Value = ManipulatorValue.brown;
                    break;
                case 2:
                    material.SetColor("_Color", Color.red);
                    Value = ManipulatorValue.red;
                    break;
                case 3:
                    material.SetColor("_Color", new Color(1, 0.561f, 0.204f));
                    Value = ManipulatorValue.orange;
                    break;
                case 4:
                    material.SetColor("_Color", Color.yellow);
                    Value = ManipulatorValue.yellow;
                    break;
                case 5:
                    material.SetColor("_Color", Color.green);
                    Value = ManipulatorValue.green;
                    break;
                case 6:
                    material.SetColor("_Color", Color.blue);
                    Value = ManipulatorValue.blue;
                    break;
                case 7:
                    material.SetColor("_Color", Color.magenta);
                    Value = ManipulatorValue.violet;
                    break;
                case 8:
                    material.SetColor("_Color", Color.gray);
                    Value = ManipulatorValue.grey;
                    break;
                case 9:
                    material.SetColor("_Color", Color.white);
                    Value = ManipulatorValue.white;
                    break;
                case 10:
                    material.SetColor("_Color", new Color(1, 0.91f, 0.318f));
                    Value = ManipulatorValue.gold;
                    break;
                case 11:
                    material.SetColor("_Color", new Color(0.702f, 0.702f, 0.702f));
                    Value = ManipulatorValue.silver;
                    break;
            }
        }
        else
        {
            if (currentValue == 5)
            {
                currentValue = 0;
            }
            switch (currentValue)
            {
                case (int)Mode.Add:
                    material.SetColor("_Color", Color.red);
                    Value = (int)Mode.Add;
                    break;
                case (int)Mode.Subtract:
                    material.SetColor("_Color", Color.red + Color.yellow);
                    Value = (int)Mode.Subtract;
                    break;
                case (int)Mode.Multiply:
                    material.SetColor("_Color", Color.green + Color.yellow);
                    Value = (int)Mode.Multiply;
                    break;
                case (int)Mode.Divide:
                    material.SetColor("_Color", Color.green);
                    Value = (int)Mode.Divide;
                    break;
                case (int)Mode.Exponent:
                    material.SetColor("_Color", Color.blue);
                    Value = (int)Mode.Exponent;
                    break;
            }
        }

        if (onValueChanged != null) onValueChanged();
    }
}
