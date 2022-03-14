using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;

namespace UniversalMachine
{
    public class PathMarker : MonoBehaviour
    {
        public class Marker
        {
            public Vector3 Position;
            public Vector3 Direction;
            public float Length;
            public Vector3 Energy;

            public Vector3 Proceed(Vector3 position, Vector3 destination, Vector3 energy)
            {
                Vector3 a = destination - position;

                Vector3 x = a.normalized;
                Vector3 y = Direction;

                float angle = Vector3.Angle(x, y);

                float attack = 1 / 180 * angle;

                Vector3 warpDirection = new Vector3(y.x * attack - x.x * attack,
                    y.y * attack - x.y * attack,
                    y.z * attack - x.z * attack);

                Vector3 warpEnergy = Energy * Length;
                Vector3 pathEnergy = energy * a.magnitude;
                Vector3 warp = new Vector3((warpEnergy.x - pathEnergy.x) * warpDirection.x,
                    (warpEnergy.y - pathEnergy.y) * warpDirection.y,
                    (warpEnergy.z - pathEnergy.z) * warpDirection.z);

                return warp;
            }
        }

        public KeyedList<Marker> Path = new KeyedList<Marker>();

        public Vector3 Project(Vector3 start, Vector3 end, Vector3 energy)
        {
            Vector3 final = end;

            foreach (Marker marker in Path)
            {
                final += marker.Proceed(start, final, energy);
            }

            Move(start, end, energy);

            return final;
        }

        public void Move(Vector3 start, Vector3 end, Vector3 energy)
        {
            Marker path = new Marker();

            path.Position = start;
            path.Direction = (end - start).normalized;
            path.Length = (end - start).magnitude;
            path.Energy = energy;

            Path.Add(path);

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}