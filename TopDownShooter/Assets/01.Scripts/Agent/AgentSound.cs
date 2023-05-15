using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    public AudioClip StepSound, HitClip, DeathClip, AttackSound;

    public void PlayStepSound()
    {
        PlayWithVariablePitch(StepSound);
    }

    public void PlayHitSound()
    {
        PlayWithVariablePitch(HitClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(DeathClip);
    }

    public void PlauAttackSound()
    {
        PlayClip(AttackSound);
    }
}
