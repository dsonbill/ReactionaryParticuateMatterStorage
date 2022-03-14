using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorSpace : MonoBehaviour
{
    public Transform Transform;

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.parent == null)
        {
            other.transform.parent.position = transform.position;
            other.transform.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            other.transform.parent.parent.position = transform.position;
            other.transform.parent.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
