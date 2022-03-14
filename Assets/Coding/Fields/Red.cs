using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Field
{
    public Black Black;

    public override void Start()
    {
        base.Start();
        FieldMaster.Red.Add(this);
    }

    void OnDestroy()
    {
        FieldMaster.Red.Remove(this);
    }

    public override void FUpdate()
    {
        Direction = Vector3.zero;
        foreach (Black black in FieldMaster.Black)
        {
            if (black == Black) continue;
            Vector3 direction = (black.transform.position - transform.position);
            Direction += new Vector3(direction.x * -black.Direction.x * (float)black.Energy, direction.y * -black.Direction.y * (float)black.Energy, direction.z * -black.Direction.z * (float)black.Energy);
        }
        foreach (Red red in FieldMaster.Red)
        {
            if (red == this) continue;
            Vector3 direction = (red.transform.position - transform.position);
            Direction -= new Vector3(direction.x * red.Direction.x * (float)red.Energy, direction.y * red.Direction.y * (float)red.Energy, direction.z * red.Direction.z * (float)red.Energy);
        }

        ApplyForce(Direction);
    }
}
