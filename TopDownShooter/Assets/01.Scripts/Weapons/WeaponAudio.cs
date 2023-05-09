using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : AudioPlayer
{
    public AudioClip ShootBulletcilp = null, OutOfBulletCilp = null, ReloadClip = null;     // 발사, 더이상 못 쏠때, 장전하는 소리

    public void PlayShootSound()
    {
        PlayWithVariablePitch(ShootBulletcilp);
    }

    public void PlayOutOfBulletSound()
    {
        PlayWithBasePitch(OutOfBulletCilp);
    }

    public void PlayReloadSound()
    {
        PlayWithBasePitch(ReloadClip);
    }
}
