using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarryMode : MonoBehaviour
{
    public static CarryableObject.Category ActiveCategory = CarryableObject.Category.Wiring;

    public Text Display;

    public static bool locked;

    private void Start()
    {
        ChangeStates();
    }

    void Update()
    {
        if (locked) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            int currentCategory = (int)ActiveCategory;
            currentCategory++;

            if (currentCategory == 4)
            {
                currentCategory = 0;
            }

            ActiveCategory = (CarryableObject.Category)currentCategory;

            switch (ActiveCategory)
            {
                case CarryableObject.Category.Wiring:
                    Display.text = "Mode: Wiring";
                    break;
                case CarryableObject.Category.Components:
                    Display.text = "Mode: Components";
                    break;
                case CarryableObject.Category.Computers:
                    Display.text = "Mode: Computers";
                    break;
                case CarryableObject.Category.Objects:
                    Display.text = "Mode: Objects";
                    break;
            }

            ChangeStates();
        }
    }

    void ChangeStates()
    {
        foreach (CarryableObject obj in CarryableObject.Carryables)
        {
            if (obj.ObjectCategory == ActiveCategory)
            {
                obj.trigger.enabled = true;
                continue;
            }
            obj.trigger.enabled = false;
        }
    }
}
