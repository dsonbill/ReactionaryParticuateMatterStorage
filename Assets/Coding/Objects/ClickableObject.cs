using UnityEngine;
using UnityEngine.EventSystems;
using System;

[AddComponentMenu("Object Interaction/ClickableObject")]
public class ClickableObject : EventTrigger
{
    public GameObject character;
    public GameObject carryPoint;
    public event Action onPointerClick;

    void Awake()
    {
    }

    // Update is called once per frame
    public void SetGazedAt(bool gazedAt)
    {
        //GetComponent<Renderer>().material.color = gazedAt ? Color.cyan : originalColor;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        SetGazedAt(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        SetGazedAt(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (onPointerClick != null) onPointerClick();
    }
}