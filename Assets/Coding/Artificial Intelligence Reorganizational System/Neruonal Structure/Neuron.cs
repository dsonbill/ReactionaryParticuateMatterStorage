using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{

    public class Jumper
    {
        public int Strand;
        public int Junction;

        public Neuron Destination;
        public Neuron Origin;

        public Jumper(int strand, int junction, Neuron origin, Neuron dest)
        {
            Strand = strand;
            Junction = junction;
            Destination = dest;
            Origin = origin;
        }

        public void Jump()
        {
            Destination.Space.Junction[Junction] += Origin.Space.Strand[Strand];
            Origin.Space.Strand[Strand] -= Origin.Space.Strand[Strand];
        }
    }

    static public char[] available = "OGMRNHILKJPQFCED".ToCharArray();
    //static public char[] available = "RSUNGOPTLCKJQFHVEMID".ToCharArray();
    //static public char[] available = "CDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public NeuronalSpace Space;
    public MathematicProcessor Processor;
    public ChaoticOrganizer Chaos;
    public EnergyResponseSystem EnergyResponseSystem;

    public Dictionary<char, int> ReverseIdentity;

    public List<Jumper> Connections;

    public System.Random r = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        Connections = new List<Jumper>();

        ReverseIdentity = new Dictionary<char, int>();
        for (int i = 0; i < Space.Junction.Length; i++)
        {
            char x = RandomIdentity();
            ReverseIdentity[x] = i;
        }
    }

    void Jump()
    {
        foreach (Jumper connection in Connections)
        {
            connection.Jump();
        }
    }

    void Update()
    {
        Inject();
        Jump();
    }

    char RandomIdentity()
    {
        char x = available[r.Next(0, 19)];
        if (ReverseIdentity.ContainsKey(x))
        {
            return RandomIdentity();
        }
        return x;
    }

    void Inject()
    {
        ChaoticOrganizer.Payload x = Chaos.Pump();
        if (!ReverseIdentity.ContainsKey(x.Designation))
        {
            return;
        }
        //Debug.Log(x.Designation + ": " + x.Information);
        Space.Junction[ReverseIdentity[x.Designation]] = x.Information * EnergyResponseSystem.EnergyLevel;
        Pass();
    }

    void Pass()
    {
        List<int> ConnectionsMapped = new List<int>();
        for (int i = 0; i < Space.Strand.Length; i++)
        {
            Space.Strand[i] = Processor.Process(Space.Junction[Space.Connection[i]], Space.Strand[i]);
            ConnectionsMapped.Add(i);
        }
        int x = 0;
        for (int i = 0; i < Space.Connection.Length; i++)
        {
            if (ConnectionsMapped.Contains(i))
                continue;

            Space.Strand[Space.Connection[i]] = Processor.Process(Space.Junction[x], Space.Strand[Space.Connection[i]]);
            x++;
            
        }
    }
}
