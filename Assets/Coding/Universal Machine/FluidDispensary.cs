using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class FluidDispensary : MonoBehaviour
    {
        public Action OnDestroy;

        // 12/30/2021 : 11:34 PM
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Particle>() != null)
            Destroy(collision.gameObject);
            OnDestroy?.Invoke();
        }
    }
}