using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleImpactScript : ImpactScript
{
    private ParticleSystem[] particleSystems;
    protected override void Awake()
    {
        base.Awake();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public override void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        base.SetPositionAndRotation(pos, rot);

        StartCoroutine(DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        float maxDuration = 0;

        foreach (ParticleSystem p in particleSystems)
        {
            if (p.main.duration > maxDuration)
            {
                maxDuration = p.main.duration;
            }
        }
        yield return new WaitForSeconds(maxDuration);
        DestroyAfterAnimation();
    }
}
