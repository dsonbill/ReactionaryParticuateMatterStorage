using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronalSpace : MonoBehaviour
{
    public int Space;

    public float[] Strand;
    public float[] Junction;

    public int[] Connection;


    //Cavity
    //Junction
    //Masterful System



    // Start is called before the first frame update
    void Start()
    {
        System.Random r = new System.Random();

        Space = r.Next(2, 20);
        Connection = new int[Space];

        int strandSpace = r.Next(1, Space - 1);

        Strand = new float[strandSpace];
        Junction = new float[Space - strandSpace];

        for(int i = 0; i < strandSpace; i++)
        {
            Strand[i] = 1.175494e-38f;
        }
        for (int i = 0; i < Space - strandSpace; i++)
        {
            Junction[i] = 1.175494e-38f;
        }

        
        for (int i = 0; i < strandSpace; i++)
        {
            Connection[i] = r.Next(0, Space - strandSpace);
        }
        for (int i = strandSpace; i < Space; i++)
        {
            Connection[i] = r.Next(0, strandSpace);
        }
    }

}
