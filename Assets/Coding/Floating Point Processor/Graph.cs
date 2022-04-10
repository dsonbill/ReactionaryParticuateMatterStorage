using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FloatingPointProcessor
{
    public class Graph : MonoBehaviour
    {
        public enum PositionMode
        {
            x = 1,
            y = 6,
            z = 9
        }

        public GameObject Standard;

        public Graphing Points;
        public int PointCount;

        public Vector2 Range = Vector2.one;

        public Vector2 Position;

        public PositionMode XMode;
        public PositionMode YMode;

        public bool Negative;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Position = new Vector2(
                (Position.x > 1) ? 1 : Position.x,
                (Position.y > 1) ? 1 : Position.y);

            if (Negative)
            {
                Position = new Vector2(
                    (Position.x < -1) ? -1 : Position.x,
                    (Position.y < -1) ? -1 : Position.y);
            }
            else
            {
                Position = new Vector2(
                (Position.x < 0) ? 0 : Position.x,
                (Position.y < 0) ? 0 : Position.y);
            }

            Vector3 newPosition = Vector3.zero;
            switch ((int)XMode + (int)YMode)
            {
                case 2:
                    newPosition = new Vector3(
                        Position.x * Range.x + Position.y * Range.y,
                        Standard.transform.localPosition.y,
                        Standard.transform.localPosition.z);
                    break;

                case 12:
                    newPosition = new Vector3(
                        Standard.transform.localPosition.x,
                        Position.x * Range.x + Position.y * Range.y,
                        Standard.transform.localPosition.z);
                    break;

                case 18:
                    newPosition = new Vector3(
                        Standard.transform.localPosition.x,
                        Standard.transform.localPosition.y,
                        Position.x * Range.x + Position.y * Range.y);
                    break;


                default:
                    float x = Standard.transform.localPosition.x;
                    float y = Standard.transform.localPosition.y;
                    float z = Standard.transform.localPosition.z;

                    switch ((int)XMode)
                    {
                        case (int)PositionMode.x:
                            x = Position.x * Range.x;
                            break;

                        case (int)PositionMode.y:
                            y = Position.x * Range.x;
                            break;

                        case (int)PositionMode.z:
                            z = Position.x * Range.x;
                            break;
                    }

                    switch ((int)YMode)
                    {
                        case (int)PositionMode.x:
                            x = Position.y * Range.y;
                            break;

                        case (int)PositionMode.y:
                            y = Position.y * Range.y;
                            break;

                        case (int)PositionMode.z:
                            z = Position.y * Range.y;
                            break;
                    }

                    newPosition = new Vector3(x, y, z);
                    break;
            }

            Standard.transform.localPosition = newPosition;
        }
    }
}