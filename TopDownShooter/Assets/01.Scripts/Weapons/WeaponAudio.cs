using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : AudioPlayer
{
    public AudioClip ShootBulletcilp = null, OutOfBulletCilp = null, ReloadClip = null;     // �߻�, ���̻� �� ��, �����ϴ� �Ҹ�

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
