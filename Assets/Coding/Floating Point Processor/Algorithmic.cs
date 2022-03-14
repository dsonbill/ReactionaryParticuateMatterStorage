using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloatingPointProcessor
{
    public class Algorithmic : MonoBehaviour
    {
        public class Result
        {
            public double X;
            public double Y;

            public Result(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        public enum Mode
        {
            X,
            Y
        }

        public class Algorithm
        {

            private Algorithm(Func<double, double, double> Modeless) {this.Modeless = Modeless; }

            Func<double, double, double> Modeless { get; set; }

            public double Process(double i, double a)
            {
                return Modeless(i, a);
            }

            //public static Algorithm Name { get { return new Algorithm((i, a) => { return ; } ); } }
            public static Algorithm Natural { get { return new Algorithm((i, a) => { return i * (a + 1); } ); } }
            public static Algorithm Square { get { return new Algorithm((i, a) => { return Math.Pow(i, 2); }); } }
            public static Algorithm Root { get { return new Algorithm((i, a) => { return Math.Sqrt(i); }); } }
            public static Algorithm Sin { get { return new Algorithm((i, a) => { return Math.Sin(i); }); } }
            public static Algorithm Cos { get { return new Algorithm((i, a) => { return Math.Cos(i); }); } }
            public static Algorithm Linear { get { return new Algorithm((i, a) => { return i + 1; }); } }
            public static Algorithm Reciprocal { get { return new Algorithm((i, a) => { return 1 / i; }); } }
            public static Algorithm Log { get { return new Algorithm((i, a) => { return Math.Log(i); }); } }
            public static Algorithm Pass { get { return new Algorithm((i, a) => { return i; }); } }

            //public static Algorithm Square { get { return new Algorithm((x, y) => { return new Result(Math.Pow(x, 2), Math.Pow(y, 2)); }); } }
            //public static Algorithm SinX { get { return new Algorithm((x, y) => { return new Result(Math.Sin(x), y); }); } }
            //public static Algorithm SinY { get { return new Algorithm((x, y) => { return new Result(x, Math.Sin(y)); }); } }
            //public static Algorithm CosX { get { return new Algorithm((x, y) => { return new Result(Math.Cos(x), y); }); } }
            //public static Algorithm CosY { get { return new Algorithm((x, y) => { return new Result(x, Math.Cos(y)); }); } }


        }

        public Algorithm X;
        public Algorithm Y;

        public Result Solve(double x, double y)
        {
            double xout = X.Process(x, y);
            double yout = Y.Process(x, y);

            return new Result(xout, yout);
        }
    }
}