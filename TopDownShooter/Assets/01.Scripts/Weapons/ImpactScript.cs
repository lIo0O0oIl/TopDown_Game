using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ImpactScript : PoolableMono
{
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DestroyAfterAnimation()
    {
        PoolManager.Instance.Push(this);
    }

    public virtual void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public virtual void SetLocalScale(Vector3 scale)
    {
        transform.localPosition = scale;
    }

    public override void Init()
    {

    }
}
