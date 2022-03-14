using UnityEngine.UI;
using UnityEngine;
using System;

namespace Transmission.Components
{
    public class TGate : MonoBehaviour
    {
        public Text mode;
        public Text units;

        public Text cutoff;
        public double gateCutoff = 1;

        public Transmitter Transmitter;
        public TransmitterConnector Gate;

        public enum Mode
        {
            Flow,
            Add,
            Subtract,
            Multiply,
            Divide,
            Exponent
        }

        public class Units
        {
            public const double G = 1000000000;
            public const double M = 1000000;
            public const double k = 1000;
            public const double Standard = 1;
            public const double m = 1e-3;
            public const double μ = 1e-6;
            public const double n = 1e-9;
            public const double p = 1e-12;
        }

        public Mode CurrentMode;
        int currentUnits;

        void Start()
        {
            Transmitter.manipulator = Manipulate;
        }

        double Manipulate(double data)
        {
            if ((gateCutoff > 0 && Gate.Transmitter.rx < gateCutoff) || (gateCutoff < 0 && Gate.Transmitter.rx > gateCutoff))
            {
                return 0;
            }

            switch (CurrentMode)
            {
                case (Mode.Flow):
                    return data;
                case (Mode.Add):
                    return data + Gate.Transmitter.rx;
                case (Mode.Subtract):
                    return data - Gate.Transmitter.rx;
                case (Mode.Multiply):
                    return data * Gate.Transmitter.rx;
                case (Mode.Divide):
                    return data / Gate.Transmitter.rx;
                case (Mode.Exponent):
                    return Math.Pow(data, Gate.Transmitter.rx);
            }

            return 0;
        }

        public void ChangeMode()
        {
            if (CurrentMode == Mode.Exponent)
                CurrentMode = Mode.Flow;
            else
                CurrentMode = (Mode)((int)CurrentMode + 1);

            switch (CurrentMode)
            {
                case (Mode.Flow):
                    mode.text = "|";
                    break;
                case (Mode.Add):
                    mode.text = "+";
                    break;
                case (Mode.Subtract):
                    mode.text = "-";
                    break;
                case (Mode.Multiply):
                    mode.text = "*";
                    break;
                case (Mode.Divide):
                    mode.text = "/";
                    break;
                case (Mode.Exponent):
                    mode.text = "^";
                    break;
            }
        }

        void ValueChanged()
        {
            cutoff.text = GetText();
        }

        public void ChangeUnits()
        {
            currentUnits++;
            if (currentUnits == 8)
                currentUnits = 0;
            switch (currentUnits)
            {
                case (0):
                    units.text = "S";
                    break;
                case (1):
                    units.text = "m";
                    break;
                case (2):
                    units.text = "μ";
                    break;
                case (3):
                    units.text = "n";
                    break;
                case (4):
                    units.text = "p";
                    break;
                case (5):
                    units.text = "G";
                    break;
                case (6):
                    units.text = "M";
                    break;
                default:
                    units.text = "k";
                    break;
            }
        }

        public void UpClick()
        {
            switch (currentUnits)
            {
                case (0):
                    gateCutoff += Units.Standard;
                    break;
                case (1):
                    gateCutoff += Units.m;
                    break;
                case (2):
                    gateCutoff += Units.μ;
                    break;
                case (3):
                    gateCutoff += Units.n;
                    break;
                case (4):
                    gateCutoff += Units.p;
                    break;
                case (5):
                    gateCutoff += Units.G;
                    break;
                case (6):
                    gateCutoff += Units.M;
                    break;
                default:
                    gateCutoff += Units.k;
                    break;
            }

            ValueChanged();
        }

        public void DownClick()
        {
            switch (currentUnits)
            {
                case (0):
                    gateCutoff -= Units.Standard;
                    break;
                case (1):
                    gateCutoff -= Units.m;
                    break;
                case (2):
                    gateCutoff -= Units.μ;
                    break;
                case (3):
                    gateCutoff -= Units.n;
                    break;
                case (4):
                    gateCutoff -= Units.p;
                    break;
                case (5):
                    gateCutoff -= Units.G;
                    break;
                case (6):
                    gateCutoff -= Units.M;
                    break;
                default:
                    gateCutoff -= Units.k;
                    break;
            }

            ValueChanged();
        }

        string GetText()
        {
            if (gateCutoff >= 1000000000)
            {
                return gateCutoff / 1000000000 + " G";
            }
            else if (gateCutoff >= 1000000)
            {
                return gateCutoff / 1000000 + " M";
            }
            else if (gateCutoff >= 1000)
            {
                return gateCutoff / 1000 + " k";
            }
            else if (gateCutoff >= 1)
            {
                return gateCutoff.ToString();
            }
            else if (gateCutoff >= Units.m)
            {
                return gateCutoff / Units.m + " m";
            }
            else if (gateCutoff >= Units.μ)
            {
                return gateCutoff / Units.μ + " μ";
            }
            else if (gateCutoff >= Units.n)
            {
                return gateCutoff / Units.n + " n";
            }
            else if (gateCutoff >= Units.p)
            {
                return gateCutoff / Units.p + " p";
            }


            else if (gateCutoff <= -1000000000)
            {
                return gateCutoff / 1000000000 + " G";
            }
            else if (gateCutoff <= -1000000)
            {
                return gateCutoff / 1000000 + " M";
            }
            else if (gateCutoff <= -1000)
            {
                return gateCutoff / 1000 + " k";
            }
            else if (gateCutoff <= -1)
            {
                return gateCutoff.ToString();
            }
            else if (gateCutoff <= -Units.m)
            {
                return gateCutoff / Units.m + " m";
            }
            else if (gateCutoff <= -Units.μ)
            {
                return gateCutoff / Units.μ + " μ";
            }
            else if (gateCutoff <= -Units.n)
            {
                return gateCutoff / Units.n + " n";
            }
            else if (gateCutoff <= -Units.p)
            {
                return gateCutoff / Units.p + " p";
            }


            else return gateCutoff.ToString();
        }
    }
}