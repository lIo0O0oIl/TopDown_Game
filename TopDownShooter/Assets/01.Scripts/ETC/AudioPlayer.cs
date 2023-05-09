using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    protected AudioSource audioSource;
    [SerializeField]
    private float pitchRandomness = 0.2f, basePitch;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        basePitch = audioSource.pitch;
    }

    public void PlayWithVariablePitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-pitchRandomness, pitchRandomness);
        audioSource.pitch = basePitch + randomPitch;
        PlayClip(clip);
    }

    public void PlayWithBasePitch(AudioClip clip)
    {
        audioSource.pitch = basePitch;
        PlayClip(clip);
    }

    private void PlayClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
