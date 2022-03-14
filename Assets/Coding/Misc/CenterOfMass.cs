using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.centerOfMass = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
