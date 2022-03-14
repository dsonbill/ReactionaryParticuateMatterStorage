using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Weapon and ZigZag

public class Pattern : MonoBehaviour
{
    public Neuron Root;

    public ChaoticOrganizer Chaos;

    public EnergyResponseSystem ERS;

    public List<Neuron> Neurons = new List<Neuron>();

    Dictionary<char, int> ReverseIdentity;

    System.Random r = new System.Random();

    bool latch = false;

    int position = 0;

    bool zygote = false;

    bool born = false;

    Neuron rebirth;

    Neuron lastNeuron;

    public int length = 0;

    bool fire = false;

    NeuronalSpace space;

    MathematicProcessor processor;

    void Update()
    {
        if (latch) return;

        if (!fire)
        {
            length = Chaos.Pump().Information;

            Debug.Log("Building " + length + " Neurons");

            lastNeuron = Root;

            fire = true;
        }
        

        for (int i = position; i < length; i++)
        {
            if (!born && !zygote)
            {
                space = gameObject.AddComponent(typeof(NeuronalSpace)) as NeuronalSpace;
                processor = gameObject.AddComponent(typeof(MathematicProcessor)) as MathematicProcessor;

                zygote = true;

                return;
            }

            if (!born)
            {
                
                Neuron birth = gameObject.AddComponent(typeof(Neuron)) as Neuron;
                
                birth.Chaos = Chaos;
                birth.EnergyResponseSystem = ERS;
                birth.Space = space;
                birth.Processor = processor;
                
                rebirth = birth;
                
                born = true;
                
                return;
            }
            
            if (born)
            {
                born = false;
                zygote = false;
            }


            Neurons.Add(rebirth);

            for (int x = 0; x < lastNeuron.Space.Strand.Length; x++)
            {
                ChaoticOrganizer.Payload pld = Chaos.Pump();
                
                
                while (!rebirth.ReverseIdentity.ContainsKey(pld.Designation))
                {
                    pld = Chaos.Pump();
                }
                
                rebirth.Space.Junction[rebirth.ReverseIdentity[pld.Designation]] += pld.Information;
                
                lastNeuron.Connections.Add(new Neuron.Jumper(x, rebirth.ReverseIdentity[pld.Designation], lastNeuron, rebirth));
            }

            for (int x = 0; x < rebirth.Space.Junction.Length; x++)
            {
                ChaoticOrganizer.Payload pld = Chaos.Pump();
                

                int target = r.Next(0, lastNeuron.Space.Strand.Length);
                
                //lastNeuron.Space.Strand[target] += pld.Information;
                
                rebirth.Connections.Add(new Neuron.Jumper(target, x, lastNeuron, rebirth));
            }

            lastNeuron = rebirth;
            position++;
        }

        latch = true;

    }
}
