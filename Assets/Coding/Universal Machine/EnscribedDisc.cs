using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class EnscribedDisc : MonoBehaviour
    {
        public Transform Disc;


        public double Diameter;

        public string Enscription;

        public double RotationRate;

        public double Height;

        public CurvedText Text;


        void Start()
        {
            Text.text = Enscription;
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

        public double Energy()
        {
            return Number() / Height; 
        }

        public double Offset()
        {
            return Energy() * RotationRate / Diameter;
        }

        void Update()
        {
            Disc.localScale = new Vector3((float)Diameter * 1.45f, (float)Height * 0.03f, (float)Diameter * 1.45f);
            Disc.localPosition = new Vector3(Disc.localPosition.x, (float)Height, Disc.localPosition.z);

            Disc.Rotate(new Vector3(0, 1, 0), (float)RotationRate);
        }
    }
}