using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Transmission;

public class ReactorInterface : MonoBehaviour
{
    public ReactorSpace Reactor;

    public Transmitter ParticleCount;

    public Transmitter ActiveParticle;
    public Transmitter ActiveAxisPosition;
    public Transmitter ActiveAxisRotation;

    public Transmitter PositionOutput;
    public Transmitter RotationOutput;

    void Start()
    {
        ParticleCount.manipulator = Count;

        PositionOutput.manipulator = POutput;
        RotationOutput.manipulator = ROutput;
    }

    double Count(double data)
    {
        return ParticleTracking.Particles.Count;
    }

    double POutput(double data)
    {
        if (!ParticleTracking.Particles.ContainsKey((int)ActiveParticle.rx) || (int)ActiveAxisPosition.rx > 2)
        {
            return 0;
        }

        return ParticleTracking.Particles[(int)ActiveParticle.rx].position[(int)ActiveAxisPosition.rx] - Reactor.Transform.position[(int)ActiveAxisPosition.rx];
    }

    double ROutput(double data)
    {
        if (!ParticleTracking.Particles.ContainsKey((int)ActiveParticle.rx) || (int)ActiveAxisRotation.rx > 2)
        {
            return 0;
        }

        return ParticleTracking.Particles[(int)ActiveParticle.rx].rotation.eulerAngles[(int)ActiveAxisRotation.rx];
    }
}
