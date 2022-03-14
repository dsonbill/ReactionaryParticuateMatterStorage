using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Transmission;

namespace Threads
{
    public class CircuitThread : MonoBehaviour
    {
        public static CircuitThread Instance { get; private set; }

        bool run = true;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            PlayStateNotifier.AddPlayStateNotifier(() => { run = false; });
        }

        public void AddCircuit(Circuit circuit)
        {
            new Thread(() =>
            {
                while (run && !circuit.IsDestroyed())
                {
                    try
                    {
                        circuit.SimulateCircuit();
                    }
                    catch
                    {

                    }
                }
            }).Start();
        }

        void OnDestroy()
        {
            run = false;
        }

        void OnApplicationQuit()
        {
            run = false;
        }
    }
}