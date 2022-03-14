using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Vector3 Direction;
    public Rigidbody rb;

    public double Energy;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void FUpdate()
    {

    }

    public void ApplyForce(Vector3 force)
    {
        if (float.IsNaN(force.x) || float.IsNaN(force.y) || float.IsNaN(force.z))
        {
            return;
        }

        if (float.IsInfinity(force.x) || float.IsInfinity(force.y) || float.IsInfinity(force.z))
        {
            return;
        }

        rb.AddForce(force);
    }
}
