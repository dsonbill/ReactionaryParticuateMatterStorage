using UnityEngine;

namespace Transmission.Components
{
    public class TSwitch : MonoBehaviour
    {
        public GameObject button;
        public Vector3 onPosition;
        public Vector3 offPosition;
        public Vector3 onRotation;
        public Vector3 offRotation;

        bool internalState;

        public ClickableObject clickableObject;
        public AudioSource switchSound;

        public Transmitter Transmitter;

        void Start()
        {
            if (switchSound == null) switchSound = GetComponent<AudioSource>();

            clickableObject.onPointerClick += SwitchEvent;

            Transmitter.manipulator = Manipulate;
        }

        double Manipulate(double data)
        {
            switch (internalState)
            {
                case true:
                    return data;
                case false:
                    return 0;
            }
        }

        void SwitchEvent()
        {
            if (switchSound != null) switchSound.Play();

            internalState = !internalState;
            if (internalState)
            {
                button.transform.localPosition = onPosition;
                button.transform.localRotation = Quaternion.Euler(onRotation);
            }
            else
            {
                button.transform.localPosition = offPosition;
                button.transform.localRotation = Quaternion.Euler(offRotation);
            }
        }
    }
}