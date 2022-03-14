using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class NeuronalOutput : MonoBehaviour
{
    public Text Text;

    NeuronalSpace Space;
    Pattern Pattern;

    public Neuron Root;
    public ChaoticOrganizer Chaos;
    public EnergyResponseSystem ERS;

    public void Generate()
    {
        GameObject go = new GameObject();
        Pattern = go.AddComponent<Pattern>();

        Pattern.Root = Root;
        Pattern.Chaos = Chaos;
        Pattern.ERS = ERS;
    }

    public void Output()
    {
        if (Pattern.Neurons.Count == Pattern.length)
        {
            Space = Pattern.Neurons[Pattern.Neurons.Count - 1].Space;
            string output = "";
            for (int i = 0; i < Space.Strand.Length; i++)
            {
                output += Space.Strand[i] + "\n";
            }
            Text.text = output;
        }
    }
}
