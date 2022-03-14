using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorTerminal : MonoBehaviour
{
    //const double MaxMinusMinEnergy = 3e-06 - 8e-07;
    //const double MinEnergy = 8e-07;
    const double MaxMinusMinEnergy = 0.999;
    const double MinEnergy = .0001;

    public enum Mode
    {
        YPNS,
        SuperParticle
    }

    public enum EnergyMode
    {
        Red,
        Black
    }

    public Mode mode;
    public EnergyMode energyMode;

    public GameObject YPNS;
    public GameObject SuperParticle;

    public Text ModeButton;

    public Text EnergyLevel;

    public GameObject SpawnLocation;

    double red;
    double black;

    public void ModeChange()
    {
        if (mode == Mode.YPNS)
        {
            mode = Mode.SuperParticle;
            UpdateEnergyLevel();
            ModeButton.text = "Super\nParticle";
            return;
        }
        mode = Mode.YPNS;
        energyMode = EnergyMode.Red;
        UpdateEnergyLevel();
        ModeButton.text = "YPNS";
    }

    public void EnergyModeChange()
    {
        red = 0;
        black = 0;
        if (mode == Mode.YPNS)
        {
            energyMode = EnergyMode.Red;
            UpdateEnergyLevel();
            return;
        }
        if (energyMode == EnergyMode.Red)
        {
            energyMode = EnergyMode.Black;
            UpdateEnergyLevel();
            return;
        }
        energyMode = EnergyMode.Red;
        UpdateEnergyLevel();
    }

    public void AddEnergy()
    {
        if (energyMode == EnergyMode.Black)
        {
            black += 0.1;
            if (black > 1) black = 1;
            UpdateEnergyLevel();
            return;
        }
        red += 0.1;
        if (red > 1) red = 1;
        UpdateEnergyLevel();
    }

    public void RemoveEnergy()
    {
        if (energyMode == EnergyMode.Black)
        {
            black -= 0.1;
            if (black < -1) black = -1;
            UpdateEnergyLevel();
            return;
        }
        if (mode == Mode.SuperParticle)
        {
            red -= 0.1;
            if (red < -1) red = -1;
            UpdateEnergyLevel();
            return;
        }
        red -= 0.1;
        if (red < 0) red = 0;
        UpdateEnergyLevel();
    }

    void UpdateEnergyLevel()
    {
        if (energyMode == EnergyMode.Black)
        {
            EnergyLevel.text = black.ToString();
            return;
        }
        EnergyLevel.text = red.ToString();
    }

    public void Spawn()
    {
        if (mode == Mode.YPNS)
        {
            GameObject go = GameObject.Instantiate(YPNS);
            go.GetComponentInChildren<PositiveWellPiston>().EnergyLevel = red * MaxMinusMinEnergy + MinEnergy;
            go.transform.position = SpawnLocation.transform.position;
            go.SetActive(true);
            return;
        }
        GameObject gu = GameObject.Instantiate(SuperParticle);
        SuperParticle sp = gu.GetComponentInChildren<SuperParticle>();
        sp.PositiveEnergyLevel = red / 2;
        sp.NegativeEnergyLevel = black / 2;
        gu.transform.position = SpawnLocation.transform.position;
        gu.SetActive(true);
    }
}
