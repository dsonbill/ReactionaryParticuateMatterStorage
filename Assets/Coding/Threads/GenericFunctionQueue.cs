using System;

namespace Threads
{
    public class GenericFunctionQueue : ThreadedFunctionQueue
    {
        public static GenericFunctionQueue Instance { get; private set; }
        public override void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            threadCount = 2;

            base.Awake();
        }

        public override void LoopAction(object operation)
        {
            (operation as Action)();
        }
    }
}