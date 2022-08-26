using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.Resource
{
    public class Stage : MonoBehaviour
    {

        public enum Classification
        {
            Existor,
            Contactor,
            Reactor,
            Representor,
            Control,
            Network
        }

        public Classification Resource;
        
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