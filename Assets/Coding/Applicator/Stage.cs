using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.Resource
{
    public class Stage : MonoBehaviour
    {

        //public class Striation
        //{
        //    public class Vector
        //    {
        //        public double X, Y, Z;
        //
        //        public Vector(double x, double y, double z)
        //        {
        //            X = x;
        //            Y = y;
        //            Z = z;
        //        }
        //    }
        //
        //    public string name;
        //    public string description;
        //    public string type;
        //
        //    public Vector alpha;
        //    public Vector Omega;
        //
        //    public Vector Speed;
        //    public Vector Tilt;
        //
        //    public Vector Altitude;
        //    public Vector Bottom;
        //    public Vector Top;
        //    public Vector Center;
        //    public Vector CenterX;
        //    public Vector CenterY;
        //    public Vector CenterZ;
        //
        //    public Vector CenterW;
        //    public Vector CenterH;
        //    public Vector CenterV;
        //
        //}    

        public enum Resource
        {
            Existor,
            Contactor,
            Reactor,
            Representor,
            Control,
            Network,
            Foundation,
            Unclassified
        }

        public Resource Primary;
        public Resource Secondary;

        public string ApplicatorCode;

        public string Identity;

        public string Authorization;

        public string Description;

        public string Name;

        public string Type;

        public string Version;

        public GameObject P_Resc;
        public GameObject S_Resc;

        public string ResourceType;

        public string ResourceVersion;

        public string ResourceName;

        public string ResourceDescription;

        public string ResourceInjectionCode;


        //public string 

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