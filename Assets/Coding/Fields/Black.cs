using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : Field
{
    public Red Red;
    public override void Start()
    {
        base.Start();
        FieldMaster.Black.Add(this);
    }

    void OnDestroy()
    {
        FieldMaster.Black.Remove(this);
    }

    public override void FUpdate()
    {
        Direction = Vector3.zero;
        foreach (Red red in FieldMaster.Red)
        {
            if (red == Red) continue;
            if (red.Black != null) continue;
            Direction += (red.transform.position - transform.position) / Mathf.Pow(Vector3.Distance(red.transform.position, transform.position), 2);
        }

        foreach (Black black in FieldMaster.Black)
        {
            if (black == this) continue;
            Direction -= (black.transform.position - transform.position) / Mathf.Pow(Vector3.Distance(black.transform.position, transform.position), 2);
        }

        ApplyForce(Direction);
    }
}
