using UnityEngine;
using System.Collections;

namespace Gameplay
{
    public class ObjectTrack : MonoBehaviour {
        public Transform trackingTransform;
        public Vector3 offsets = Vector3.zero;
        public Vector3 lastPosition = Vector3.zero;

        private void Awake()
        {
            if (trackingTransform != null)
            {
                gameObject.transform.rotation = trackingTransform.rotation;
            }
        }


        // Update is called once per frame
        void LateUpdate () {
            if (trackingTransform != null && lastPosition != trackingTransform.position)
            {
                Vector3 newPosition = new Vector3();
                newPosition = trackingTransform.position;
                newPosition += trackingTransform.transform.up * offsets.y;
                newPosition += trackingTransform.transform.right * offsets.x;
                newPosition += trackingTransform.transform.forward * offsets.z;

                gameObject.transform.position = newPosition;

                lastPosition = trackingTransform.position;
            }
    	}
    }
}