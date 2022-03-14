using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TSourceInterface : MonoBehaviour
{
    public Text text;
    public Text units;
    public double EnergyLevel = 1;

    public delegate void OnValueChanged(double energyLevel);
    public OnValueChanged onValueChanged;

    public class Units
    {
        public const double G = 1000000000;
        public const double M = 1000000;
        public const double k = 1000;
        public const double Standard = 1;
        public const double m = 1e-3;
        public const double μ = 1e-6;
        public const double n = 1e-9;
        public const double p = 1e-12;
    }

    int currentUnits;

    void Start()
    {
        onValueChanged += ValueChanged;
    }

    void ValueChanged(double energyLevel)
    {
        text.text = GetText();
    }

    public void UpClick()
    {
        switch (currentUnits)
        {
            case (0):
                EnergyLevel += Units.Standard;
                break;
            case (1):
                EnergyLevel += Units.m;
                break;
            case (2):
                EnergyLevel += Units.μ;
                break;
            case (3):
                EnergyLevel += Units.n;
                break;
            case (4):
                EnergyLevel += Units.p;
                break;
            case (5):
                EnergyLevel += Units.G;
                break;
            case (6):
                EnergyLevel += Units.M;
                break;
            default:
                EnergyLevel += Units.k;
                break;
        }

        if (onValueChanged != null) onValueChanged(EnergyLevel);
    }

    public void DownClick()
    {
        switch (currentUnits)
        {
            case (0):
                EnergyLevel -= Units.Standard;
                break;
            case (1):
                EnergyLevel -= Units.m;
                break;
            case (2):
                EnergyLevel -= Units.μ;
                break;
            case (3):
                EnergyLevel -= Units.n;
                break;
            case (4):
                EnergyLevel -= Units.p;
                break;
            case (5):
                EnergyLevel -= Units.G;
                break;
            case (6):
                EnergyLevel -= Units.M;
                break;
            default:
                EnergyLevel -= Units.k;
                break;
        }

        if (onValueChanged != null) onValueChanged(EnergyLevel);
    }

    public void ChangeUnits()
    {
        currentUnits++;
        if (currentUnits == 8)
            currentUnits = 0;
        switch (currentUnits)
        {
            case (0):
                units.text = "S";
                break;
            case (1):
                units.text = "m";
                break;
            case (2):
                units.text = "μ";
                break;
            case (3):
                units.text = "n";
                break;
            case (4):
                units.text = "p";
                break;
            case (5):
                units.text = "G";
                break;
            case (6):
                units.text = "M";
                break;
            default:
                units.text = "k";
                break;
        }
    }

    string GetText()
    {
        if (EnergyLevel >= 1000000000)
        {
            return EnergyLevel / 1000000000 + " G";
        }
        else if (EnergyLevel >= 1000000)
        {
            return EnergyLevel / 1000000 + " M";
        }
        else if (EnergyLevel >= 1000)
        {
            return EnergyLevel / 1000 + " k";
        }
        else if (EnergyLevel >= 1)
        {
            return EnergyLevel.ToString();
        }
        else if (EnergyLevel >= Units.m)
        {
            return EnergyLevel / Units.m + " m";
        }
        else if (EnergyLevel >= Units.μ)
        {
            return EnergyLevel / Units.μ + " μ";
        }
        else if (EnergyLevel >= Units.n)
        {
            return EnergyLevel / Units.n + " n";
        }
        else if (EnergyLevel >= Units.p)
        {
            return EnergyLevel / Units.p + " p";
        }


        else if (EnergyLevel <= -1000000000)
        {
            return EnergyLevel / 1000000000 + " G";
        }
        else if (EnergyLevel <= -1000000)
        {
            return EnergyLevel / 1000000 + " M";
        }
        else if (EnergyLevel <= -1000)
        {
            return EnergyLevel / 1000 + " k";
        }
        else if (EnergyLevel <= -1)
        {
            return EnergyLevel.ToString();
        }
        else if (EnergyLevel <= -Units.m)
        {
            return EnergyLevel / Units.m + " m";
        }
        else if (EnergyLevel <= -Units.μ)
        {
            return EnergyLevel / Units.μ + " μ";
        }
        else if (EnergyLevel <= -Units.n)
        {
            return EnergyLevel / Units.n + " n";
        }
        else if (EnergyLevel <= -Units.p)
        {
            return EnergyLevel / Units.p + " p";
        }
        

        else return EnergyLevel.ToString();
    }
}
