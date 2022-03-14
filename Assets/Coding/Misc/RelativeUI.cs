using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelativeUI : MonoBehaviour
{
    public RectTransform UIElement;

    public Vector2 RelativePosition;

    int[] lastResolution = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.currentResolution.width != lastResolution[0] || Screen.currentResolution.width != lastResolution[1])
        {
            lastResolution[0] = Screen.currentResolution.width;
            lastResolution[1] = Screen.currentResolution.height;

            //UIElement.position = new Vector2(lastResolution[0] * RelativePosition.x, lastResolution[1] * RelativePosition.y);
            UIElement.SetLeft(lastResolution[0] * RelativePosition.x);
            UIElement.SetTop(lastResolution[1] * -RelativePosition.y);
        }
    }
}
