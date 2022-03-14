using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloatingPointProcessor
{
    public class Flags : MonoBehaviour
    {
        public bool Precision;

        public bool Doubler;
        public bool Scrambler;
        public bool Digitizer;
        public bool Reprocess;


        public bool Collisions;
        public float CollisionRate;

        public bool LossInducer;
    }
}