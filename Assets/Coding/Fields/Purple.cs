using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple : Field
{

    public override void Start()
    {
        Direction = -Vector3.one;
        base.Start();
        FieldMaster.Purple.Add(this);
    }

    void OnDestroy()
    {
        FieldMaster.Purple.Remove(this);
    }

    public override void FUpdate()
    {
        Direction = Vector3.zero;
        foreach (Red red in FieldMaster.Red)
        {
            Direction -= (red.transform.position - transform.position) / Mathf.Pow(Vector3.Distance(red.transform.position, transform.position), 2);
        }

        foreach (Black black in FieldMaster.Black)
        {
            Direction += (black.transform.position - transform.position) / (float)(Mathf.Pow(Vector3.Distance(black.transform.position, transform.position), 2) * Energy);
        }

        ApplyForce(Direction);
    }
}
