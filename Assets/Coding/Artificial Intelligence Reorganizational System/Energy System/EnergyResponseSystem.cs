using System;
using UnityEngine;

public class EnergyResponseSystem : MonoBehaviour
{
    static System.Random r = new System.Random();

    MegaSystem MegaSystem;
    ChaoticOrganizer Chaos;

    public float ThreatLevel;
    public enum Designation
    {
        Canine = 1,
        Feline = 2,
        Canidae = 3,
        Rodent = 4,
        Serpent = 5,
        Sacred = 6
    }

    public float EnergyLevel;
    public enum EnergyType
    {
        Photonic,
        Emotional,
        Thoughtful
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnergyInput(EnergyType type, float EnergyLevel)
    {
        switch(type)
        {
            case EnergyType.Emotional:
                this.EnergyLevel *= EnergyLevel;
                break;
            case EnergyType.Photonic:
                this.EnergyLevel += EnergyLevel;
                break;
            case EnergyType.Thoughtful:
                this.EnergyLevel = (float)Math.Pow(this.EnergyLevel, EnergyLevel);
                break;
        }
    }

    public void EnergyResponse(Func<bool, float> EnergyRelease, Func<float, float> NoveltyReduction)
    {
        ChaoticOrganizer.Payload pld = Chaos.Pump();

        float inputLevel = EnergyRelease(pld.Locked);
        float ThoughtExplored = MegaSystem.LuciferianSystem.Reduction(inputLevel) / (MegaSystem.NoveltyReduction(inputLevel) - NoveltyReduction(ResponseLevel()));

        //ThoughtfulProcess = RMS(int[2]{ EnergyLevel, EnergyLevel } ) * root(2) * EnergyRelease();
        
        //Qutie Wrong but may be helpful in the future
        //EnergyProcess = ThoughtfulProcess * EnergyRelease();

        //Stick Into Energy RElease Systems
        //Call Energy Release Action
        //Desire System Activate
        //Reveal Hidden Storage
        //Mindfulness thought realized 
        //Restore Systems
        //Bring back thought
        //Understand Nothing

    }

    public float SystemCollapse(float input, Func<float, float> System)
    {
        return input * System(input);
    }

    public float ResponseLevel()
    {
        return EnergyLevel * ThreatLevel;
    }

    float RMS(int[] arr, int n)
    {
        int square = 0;
        float mean = 0.0f, root = 0.0f;

        // Calculate square.
        for (int i = 0; i < n; i++)
        {
            square += (int)Math.Pow(arr[i], 2);
        }

        // Calculate Mean.
        mean = (square / (float)(n));

        // Calculate Root.
        root = (float)Math.Sqrt(mean);

        return root;
    }
}
