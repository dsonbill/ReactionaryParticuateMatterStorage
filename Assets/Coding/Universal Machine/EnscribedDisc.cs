using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class EnscribedDisc : MonoBehaviour
    {
        public Transform Disc;


        public float Diameter;

        public string Enscription;

        public double RotationRate;

        public float Height;

        public float Rotation;

        public Vector3 Flow;

        public CurvedText Text;


        void Start()
        {
            Text.text = Enscription;
            Flow = Disc.up + Disc.right;
        }

        public int Number()
        {
            char[] enscr = Enscription.ToCharArray();

            int i = 0;
            foreach (char c in enscr)
            {
                i += c - 65;
            }

            return i;
        }

        public float Energy()
        {
            return Number() / Height * Diameter; 
        }

        public Vector3 Offset()
        {
            return Energy() * (Flow * Rotation);
        }

        void Update()
        {
            Disc.localScale = new Vector3((float)Diameter * 1.45f, (float)Height * 0.03f, (float)Diameter * 1.45f);
            Disc.localPosition = new Vector3(Disc.localPosition.x, (float)Height, Disc.localPosition.z);

            Disc.Rotate(new Vector3(0, 1, 0), (float)RotationRate);

            Rotation = Disc.localRotation.y;
        }
    }
}