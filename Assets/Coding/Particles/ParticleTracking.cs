using Misc;
using UnityEngine;

public class ParticleTracking : MonoBehaviour
{
    static public KeyedList<Rigidbody> Particles = new KeyedList<Rigidbody>();
    int index;


    void Start()
    {
        index = Particles.Add(GetComponent<Rigidbody>());
    }

    private void OnDestroy()
    {
        Particles.Remove(index);
        //Particles.Compress();
    }
}
