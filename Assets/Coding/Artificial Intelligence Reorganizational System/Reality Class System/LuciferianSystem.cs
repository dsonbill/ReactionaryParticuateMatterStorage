using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Alterous & ZigZag

public class LuciferianSystem : MonoBehaviour
{
    public enum Separation
    {
        Entirely,
        Partially,
        Indiscernably,
        Inseparable
    }

    public enum Chasteness
    {
        Pure = 3,
        Above = 2,
        Central = 1,
        Below = -1,
        Impure = -2
    }

    public enum Opposition
    {
        OnlyFriend = 4,
        AlmostThere = 3,
        TheFuckDoesThatMean = 2,
        WhyDoTheyThinkThatWay = 1,
        EntirelyUntrustworthy = 0,
        IFuckingHateThem = -1,
        IKillThemForFun = -2
    }


    public Separation ClassSeparation;
    public Chasteness ClassChasteness;
    public Opposition ClassOpposition;

    public float Reduction(float input)
    {
        return (input * (int)ClassSeparation) + (input / (int)ClassChasteness) + ((float)Math.Pow(input, (int)ClassOpposition));
    }

    public int ClassNumber()
    {
        if (ClassOpposition == 0)
        {
            return 0;
        }
        return (int)ClassSeparation + (int)ClassChasteness + (int)ClassOpposition;
    }
}
