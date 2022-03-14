using System;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    ToolObject[] Layout = new ToolObject[6];

    public List<GameObject> toolObjects = new List<GameObject>();
    public string CurrentTool { get; private set; }
    public event Action<string> OnToolChange;
    public float ChangeSpeed = 5f;

    int currentToolPart;
    bool toolLoaded = true;

    List<Action> Tools = new List<Action>();
    int currentTool;

    // Start is called before the first frame update
    void Start()
    {
        Tools.Add(NoTool);
        Tools.Add(Gun1);
        Tools.Add(Gun);
    }

    public void NoTool()
    {
        Layout[0] = new ToolObject(toolObjects[0]);
        Layout[1] = new ToolObject(toolObjects[1]);
        Layout[2] = new ToolObject(toolObjects[2]);
        Layout[3] = new ToolObject(toolObjects[3]);
        Layout[4] = new ToolObject(toolObjects[4]);
        Layout[5] = new ToolObject(toolObjects[5]);
        ChangeTool("NoTool");
    }

    public void Gun1()
    {
        Layout[0] = new ToolObject(toolObjects[0], new Vector3(), Quaternion.identity, new Vector3(0.24f, 0.35f, 0.77f), new Color(0f, 0f, 1f, 0.5f));
        Layout[1] = new ToolObject(toolObjects[1], new Vector3(0f, -0.296f, -0.206f), Quaternion.Euler(26.65f, 0f, 0f), new Vector3(0.14f, 0.43f, 0.31f), new Color(1f, 1f, 1f, 0.5f));
        Layout[2] = new ToolObject(toolObjects[2], new Vector3(0f, 0f, 0.29f), Quaternion.identity, new Vector3(0.1f, 0.1f, 0.36f), new Color(1f, 1f, 1f, 0.5f));
        Layout[3] = new ToolObject(toolObjects[3]);
        Layout[4] = new ToolObject(toolObjects[4]);
        Layout[5] = new ToolObject(toolObjects[5]);
        ChangeTool("Gun1");
    }

    public void Gun()
    {
        Layout[0] = new ToolObject(toolObjects[0], new Vector3(), Quaternion.identity, new Vector3(0.33f, 0.44f, 1f), new Color(0f, 0f, 1f, 0.5f));
        Layout[1] = new ToolObject(toolObjects[1], new Vector3(0f, -0.433f, 0f), Quaternion.identity, new Vector3(0.14f, 0.43f, 0.31f), new Color(1f, 1f, 1f, 0.5f));
        Layout[2] = new ToolObject(toolObjects[2], new Vector3(0f, 0f, 0.438f), Quaternion.identity, new Vector3(0.1f, 0.1f, 1f), new Color(1f, 1f, 1f, 0.5f));
        Layout[3] = new ToolObject(toolObjects[3]);
        Layout[4] = new ToolObject(toolObjects[4]);
        Layout[5] = new ToolObject(toolObjects[5]);
        ChangeTool("Gun");
    }

    public void Bomb()
    {
        Layout[0] = new ToolObject(toolObjects[0], new Vector3(0f, 0f, 0.65f), Quaternion.identity, new Vector3(0.54f, 1.17f, 0.56f), new Color(1f, 1f, 1f, 0.5f));
        Layout[1] = new ToolObject(toolObjects[1], new Vector3(0f, 0f, 0.4f), Quaternion.identity, new Vector3(0.5f, 0.63f, 0.43f), new Color(0.5f, 0.5f, 0.5f, 0.5f));
        Layout[2] = new ToolObject(toolObjects[2]);
        Layout[3] = new ToolObject(toolObjects[3]);
        Layout[4] = new ToolObject(toolObjects[4]);
        Layout[5] = new ToolObject(toolObjects[5]);
        ChangeTool("Bomb");
    }

    void ChangeTool(string name)
    {
        CurrentTool = name;
        toolLoaded = false;
        currentToolPart = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentTool++;
            if (currentTool == Tools.Count)
            {
                currentTool = 0;
            }
            Tools[currentTool]();
        }
    }

    void FixedUpdate()
    {
        if (!toolLoaded)
        {
            bool done = Layout[currentToolPart].Update(ChangeSpeed);

            if (done) currentToolPart++;
            if (currentToolPart == 6)
            {
                OnToolChange?.Invoke(CurrentTool);
                toolLoaded = true;
            }
        }
    }
}

public class ToolObject
{
    public bool enabled { get; private set; }
    public GameObject gameObject { get; private set; }
    public Vector3 position { get; private set; }
    public Quaternion rotation { get; private set; }
    public Vector3 scale { get; private set; }
    public Color color { get; private set; }

    float posLerp = 0f;
    float scaleLerp = 0f;

    public ToolObject(GameObject gameObject, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
    {
        Initialize(true, gameObject, position, rotation, scale, color);
    }

    public ToolObject(GameObject gameObject)
    {
        Initialize(false, gameObject, Vector3.zero, Quaternion.identity, Vector3.zero, Color.white);
    }

    protected void Initialize(bool enabled, GameObject gameObject, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
    {
        this.enabled = enabled;
        this.gameObject = gameObject;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.color = color;
    }

    public bool Update(float speed)
    {
        if (gameObject.GetComponent<MeshRenderer>().material.color != color) gameObject.GetComponent<MeshRenderer>().material.color = color;
        if (gameObject.transform.localPosition != position) gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, position, posLerp);
        if (gameObject.transform.localRotation != rotation) gameObject.transform.localRotation = rotation;
        if (gameObject.transform.localScale != scale) gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, scale, scaleLerp);
        posLerp += Time.deltaTime * speed;
        scaleLerp += Time.deltaTime * speed;

        return (gameObject.transform.localPosition == position && gameObject.transform.localRotation == rotation && gameObject.transform.localScale == scale);
    }
}