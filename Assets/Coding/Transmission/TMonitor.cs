using UnityEngine;
using UnityEngine.UI;

namespace Transmission.Components
{
    public class TMonitor : MonoBehaviour
    {
        public Transmitter Transmitter;
        public Graphing Graph;
        public Text Text;

        public bool InstantReadout;
        public float WaitTime = 0.01f;

        float lastReadout = 0;

        void Update()
        {
            if (!InstantReadout)
            {
                if (lastReadout >= WaitTime)
                {
                    Graph.AddValue((float)Transmitter.rx);
                    Text.text = Transmitter.rx.ToString();

                    lastReadout = 0;
                    return;
                }

                lastReadout += Time.deltaTime;
                return;
            }

            Graph.AddValue((float)Transmitter.rx);
            Text.text = Transmitter.rx.ToString();
        }

        public void ReadoutMode()
        {
            InstantReadout = !InstantReadout;
        }
    }
}