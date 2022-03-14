using UnityEngine;
using System;
using Threads;

namespace Computers
{
    public class HardDriveWriteOperation
    {
        public Action writeAction;
    }

    [AddComponentMenu("Computers/Hard Drive Write Operator")]
    public class HardDriveWriteOperator : ThreadedFunctionQueue
    {
        public static ThreadedFunctionQueue Instance { get; set; }

        //public override void Update()
        //{
            //if (!LoadingStage.ProcessorOperatorLoaded) return;
            //base.Update();
        //}

        public override void Initialize()
        {
            if (Instance != null) return;
            Instance = this;

            threadCount = 1;

            base.Initialize();
            //LoadingStage.HardDriveWriteOperatorLoaded = true;
        }

        public override void LoopAction(object operation)
        {
            HardDriveWriteOperation currentOperation = operation as HardDriveWriteOperation;
            if (currentOperation == null) return;

            try
            {
                currentOperation.writeAction();
            }
            catch (Exception e)
            {
                Debug.Log("Exception while writing drive to disk!");
            }
        }
    }
}
