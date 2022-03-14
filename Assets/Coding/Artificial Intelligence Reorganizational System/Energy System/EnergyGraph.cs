using UnityEngine.UI;
using UnityEngine;


class EnergyGraph : MonoBehaviour
{

    public Graphing Graph;
    public EnergyResponseSystem ERS;

    public Text Text;

    float input = 1;

    public void PhotonicInput()
    {
        ERS.EnergyInput(EnergyResponseSystem.EnergyType.Photonic, input);
    }

    public void EmotionalInput()
    {
        ERS.EnergyInput(EnergyResponseSystem.EnergyType.Emotional, input);
    }

    public void ThoughtfulInput()
    {
        ERS.EnergyInput(EnergyResponseSystem.EnergyType.Thoughtful, input);
    }

    public void EnergyUp()
    {
        input += 1;
    }

    public void EnergyDown()
    {
        input -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        Graph.AddValue(ERS.EnergyLevel);
        Text.text = input.ToString();
    }
}
