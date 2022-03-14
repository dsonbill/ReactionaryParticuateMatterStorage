using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpandToTextLines : MonoBehaviour
{
    public Text text;

    public void Expand()
    {
        GetComponent<LayoutElement>().preferredHeight = text.preferredHeight / 2;
    }
}
