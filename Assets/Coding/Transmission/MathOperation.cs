using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Transmission.Components
{
    public class MathOperation : MonoBehaviour
    {
        public enum Operation
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Exponent
        }

        public Transmitter Left;
        public Transmitter Right;

        public Transmitter Result;

        public Operation Mode;

        public Text Indicator;

        // Start is called before the first frame update
        void Start()
        {
            Result.manipulator = Manipulate;
        }

        public void ChangeMode()
        {
            int currentMode = (int)Mode;
            currentMode++;
            if (currentMode == 5)
            {
                currentMode = 0;
            }

            Mode = (Operation)currentMode;

            switch (Mode)
            {
                case Operation.Addition:
                    Indicator.text = "+";
                    break;
                case Operation.Subtraction:
                    Indicator.text = "-";
                    break;
                case Operation.Multiplication:
                    Indicator.text = "*";
                    break;
                case Operation.Division:
                    Indicator.text = "/";
                    break;
                case Operation.Exponent:
                    Indicator.text = "^";
                    break;
            }
        }

        double Manipulate(double data)
        {
            switch (Mode)
            {
                case Operation.Addition:
                    return Left.rx + Right.rx;
                case Operation.Subtraction:
                    return Left.rx - Right.rx;
                case Operation.Multiplication:
                    return Left.rx * Right.rx;
                case Operation.Division:
                    return Left.rx / Right.rx;
                case Operation.Exponent:
                    return Math.Pow(Left.rx, Right.rx);
            }

            return 0;
        }
    }
}