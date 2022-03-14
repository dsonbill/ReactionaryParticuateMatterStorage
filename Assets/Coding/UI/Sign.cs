using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public Text SignText;

    void Start()
    {
        SignText.text = gameObject.name;
    }
}
