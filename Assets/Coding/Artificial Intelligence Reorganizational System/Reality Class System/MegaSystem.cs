using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSystem : MonoBehaviour
{
    public LuciferianSystem LuciferianSystem;
    public EnergyResponseSystem EnergyResponseSystem;

    public float LevelOfNovelty;
    public float NoveltyAwareness;

    //Novelty Reduction System
    //Classness Of Novelty
    //Restoration Processies
    //Novelty Acceptance Program
    //Novelty Cluelessness

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NoveltyAcceptance();
    }

    public float NoveltyReduction(float input)
    {
        return ((EnergyResponseSystem.EnergyLevel / LevelOfNovelty) * NoveltyAwareness) * input;
    }

    public void NoveltyAcceptance()
    {
        NoveltyAwareness = ScaleBetweenNumber(LuciferianSystem.ClassNumber(), -4, 10, 0, 1);
    }

    float ScaleBetweenNumber(float measurement, float measMin, float measMax, float scaleMin, float scaleMax)
    {
        return ((measurement - measMin) / (measMax - measMin)) * (scaleMax - scaleMin) + scaleMin;
    }
}
