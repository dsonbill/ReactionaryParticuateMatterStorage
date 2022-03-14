using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public static class CarryableObjectTriggerLock
{
    public static List<CarryableObjectTrigger> triggers = new List<CarryableObjectTrigger>();

    public static void LockTriggers()
    {
        foreach (CarryableObjectTrigger trigger in triggers)
        {
            trigger.enabled = false;
        }
    }

    public static void UnlockTriggers()
    {
        foreach (CarryableObjectTrigger trigger in triggers)
        {
            trigger.enabled = true;
        }
    }
}

public class CarryableObjectTrigger : EventTrigger
{
    public Guid guid = Guid.NewGuid();
    public Vector3 offset = Vector3.zero;
    public GameObject carryPoint;
    public bool connected;
    public Rigidbody rb;
    public Transform movementTransform;
    public Mode mode;
    public Dictionary<GameObject, int> layers;
    public int noCollisionLayer = 11;
    public GameObject topLayerChange;
    public Vector3 targetPosition = Vector3.forward;
    public List<Connector> Connectors;

    public enum Mode
    {
        XRot,
        YRot,
        ZRot,
        XScale,
        YScale,
        ZScale
    }
    //public Transform originalParent;
    //public Color originalColor;
    //
    //public void SetGazedAt(bool gazedAt)
    //{
    //    //GetComponent<Renderer>().material.color = gazedAt ? Color.cyan : originalColor;
    //}
    //
    //public override void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (connected) return;
    //    base.OnPointerEnter(eventData);
    //    SetGazedAt(true);
    //}
    //
    //public override void OnPointerExit(PointerEventData eventData)
    //{
    //    if (connected) return;
    //    base.OnPointerExit(eventData);
    //    SetGazedAt(false);
    //}

    void Start()
    {
        CarryableObjectTriggerLock.triggers.Add(this);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (connected) return;
        base.OnPointerClick(eventData);
        Connect();
    }

    public void Connect()
    {
        foreach (Connector connector in Connectors)
        {
            if (connector.disconnectable && connector.connected)
            {
                connector.Disconnect(); //I forgot why, but it looks important //It's pretty obvious actually

                // Easy connector disconnect condition logic
                // If the connector refused to disconnect, we cannot connect the object c
                if (connector.connected) return;
            }
        }

        layers = new Dictionary<GameObject, int>();
        SaveLayers(topLayerChange);

        rb.isKinematic = true;

        SetCarryPoint();

        offset = Vector3.zero;

        connected = true;
    }

    public void Disconnect(bool disable)
    {
        if (disable) enabled = false;

        rb.isKinematic = false;

        targetPosition = Vector3.forward;

        ControlledCharacter.ActiveCharacter.ClickShield.gameObject.SetActive(false);

        connected = false;
        ApplyLayers(topLayerChange);
    }

    public void SaveLayers(GameObject obj)
    {
        layers[obj] = obj.layer;
        obj.layer = noCollisionLayer;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            SaveLayers(obj.transform.GetChild(i).gameObject);
        }
    }

    public void ApplyLayers(GameObject obj)
    {
        if (layers.ContainsKey(obj)) obj.layer = layers[obj];
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ApplyLayers(obj.transform.GetChild(i).gameObject);
        }
    }

    void SetCarryPoint()
    {
        carryPoint = ControlledCharacter.ActiveCharacter.GetComponent<ObjectCarryPoint>().CarryPoint;
        ControlledCharacter.ActiveCharacter.ClickShield.gameObject.SetActive(true);
        carryPoint.transform.position = movementTransform.position;
        carryPoint.transform.rotation = movementTransform.rotation;
    }

    public void Update()
    {
        if (!connected) return;
        if (Input.GetButtonDown("Fire1")) Disconnect(false);

        if (Input.GetButtonDown("Fire3"))
        {
            if ((int)mode == 2) mode = 0;
            else mode += 1;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            switch (mode)
            {
                case Mode.XRot:
                    carryPoint.transform.Rotate(new Vector3(1, 0, 0), Input.GetAxis("Mouse ScrollWheel") * 12, UnityEngine.Space.Self);
                    break;

                case Mode.YRot:
                    carryPoint.transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Mouse ScrollWheel") * 12, UnityEngine.Space.Self);
                    break;

                case Mode.ZRot:
                    carryPoint.transform.Rotate(new Vector3(0, 0, 1), Input.GetAxis("Mouse ScrollWheel") * 12, UnityEngine.Space.Self);
                    break;

                case Mode.XScale:
                    movementTransform.localScale = ((Input.GetAxis("Mouse ScrollWheel") * 2) * Vector3.right) + movementTransform.localScale;
                    break;
                case Mode.YScale:
                    movementTransform.localScale = ((Input.GetAxis("Mouse ScrollWheel") * 2) * Vector3.up) + movementTransform.localScale;
                    break;
                case Mode.ZScale:
                    movementTransform.localScale = ((Input.GetAxis("Mouse ScrollWheel") * 2) * Vector3.forward) + movementTransform.localScale;
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
        if (!connected) return;

        movementTransform.rotation = carryPoint.transform.rotation;
        movementTransform.position = targetPosition;

        targetPosition = carryPoint.transform.position + offset;

        foreach (Connector connector in Connectors)
        {
            connector.CheckForConnection();
        }
    }

    public void OnDestroy()
    {
        if (connected) Disconnect(false);
    }
}

[AddComponentMenu("Object Interaction/CarryableObject")]
public class CarryableObject : MonoBehaviour
{
    public static List<CarryableObject> Carryables = new List<CarryableObject>();

    public enum Category
    {
        Wiring,
        Components,
        Computers,
        Objects
    }

    public CarryableObjectTrigger trigger;
    public Rigidbody rb;
    public Transform movementTransform;
    public GameObject topLayerChange;
    public List<Connector> Connectors;
    public Category ObjectCategory;

    void Start()
    {
        trigger = gameObject.AddComponent<CarryableObjectTrigger>();

        if (topLayerChange == null) topLayerChange = this.gameObject;
        trigger.topLayerChange = topLayerChange;

        if (rb == null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) this.rb = rb;
            else if (gameObject.transform.parent != null)
            {
                this.rb = gameObject.transform.parent.GetComponent<Rigidbody>();
            }
        }

        trigger.rb = rb;

        if (movementTransform == null) movementTransform = transform;
        trigger.movementTransform = movementTransform;


        if (Connectors.Count == 0 && GetComponent<Connector>() != null) Connectors.Add(GetComponent<Connector>());
        trigger.Connectors = Connectors;

        Carryables.Add(this);

        if (CarryMode.ActiveCategory != ObjectCategory) trigger.enabled = false;
    }
}