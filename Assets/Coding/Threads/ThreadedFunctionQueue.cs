using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Threads
{
    public class ThreadedFunctionQueue : MonoBehaviour
    {
        public bool run { get; set; }
        public int threadCount { get; set; }
        public int threadLoad { get; set; }

        private Dictionary<int, Action<object>> queueFunction = new Dictionary<int, Action<object>>();
        private Mutex functionMutex = new Mutex();
        private object runLock = new object();
        private Mutex loopMutex = new Mutex();
        
        private int currentThread;

        public virtual void Awake()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            run = true;
            if (threadCount == 0) threadCount = (Environment.ProcessorCount / 2);
            currentThread = 0;
            ThreadStart();
        }

        public virtual void ThreadStart()
        {
            for (int i = 0; i < threadCount; i++)
            {
                int thisThread = i;
                new Thread(() =>
                {
                    Stack<object> operationQueue = new Stack<object>();
                    Mutex queueLock = new Mutex();

                    functionMutex.WaitOne();
                    queueFunction[thisThread] = ((object operation) =>
                    {
                        queueLock.WaitOne();
                        operationQueue.Push(operation);
                        queueLock.ReleaseMutex();
                    });
                    functionMutex.ReleaseMutex();

                    Func<object> popFunction = () =>
                    {
                        queueLock.WaitOne();
                        object obj = operationQueue.Pop();
                        queueLock.ReleaseMutex();
                        return obj;
                    };

                    Func<bool> countFunction = () =>
                    {
                        queueLock.WaitOne();
                        int count = operationQueue.Count;
                        queueLock.ReleaseMutex();
                        return count > 0;
                    };

                    while (run)
                    {
                        try
                        {
                            while (countFunction())
                            {
                                SafeLoop(popFunction());
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                        }
                    }
                }).Start();
            }
        }

        private void SafeLoop(object operation)
        {
            loopMutex.WaitOne();
            LoopAction(operation);
            loopMutex.ReleaseMutex();
        }

        public virtual void LoopAction(object operation) { }

        public virtual void QueueOperation(object operation)
        {
            functionMutex.WaitOne();

            currentThread++;
            if (currentThread == threadCount)
            {
                currentThread = 0;
            }
            
            queueFunction[currentThread](operation);

            functionMutex.ReleaseMutex();
        }

        public void OnDestroy()
        {
            run = false;
        }

        void OnApplicationQuit()
        {
            run = false;
        }
    }
}
