using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

namespace Threads
{
    class UnityFunctionQueue : MonoBehaviour
    {
        public static UnityFunctionQueue Instance { get; private set; }

        private Stack<UnityOperationWrapper> operationQueue = new Stack<UnityOperationWrapper>();
        private Mutex queueLock = new Mutex();

        public void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void Update()
        {
            while (operationQueue.Count > 0)
            {
                queueLock.WaitOne();
                UnityOperationWrapper currentOperation = PopOperation();
                try
                {
                    currentOperation.action(currentOperation.parameters);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                queueLock.ReleaseMutex();
            }
        }

        public virtual UnityOperationWrapper PopOperation()
        {
            if (operationQueue.Count == 0) return null;

            return operationQueue.Pop();
            
        }

        public virtual void QueueOperation(UnityOperationWrapper operation)
        {
            queueLock.WaitOne();
            operationQueue.Push(operation);
            queueLock.ReleaseMutex();
        }
    }

    public class UnityOperationWrapper
    {
        public Action<object[]> action { get; set; }
        public object[] parameters { get; set; }

        public UnityOperationWrapper(Action<object[]> action, params object[] parameters)
        {
            this.action = action;
            this.parameters = parameters;
        }

        public void Call()
        {
            action(parameters);
        }
    }
}
