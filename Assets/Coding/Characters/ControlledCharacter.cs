using UnityEngine;
using System.Collections;

public class ControlledCharacter : MonoBehaviour
{

    public static ControlledCharacter ActiveCharacter;
    public static GameObject ActiveCamera;
    public GameObject InitialActiveCamera;
    public GameObject ClickShield;

    public AudioSource AudioSource;

    void Start()
    {
        if (ActiveCharacter == null) ActiveCharacter = this;
        if (ActiveCamera == null) ActiveCamera = InitialActiveCamera;

        enabled = false;
    }
}