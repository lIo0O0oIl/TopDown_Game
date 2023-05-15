using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected Transform muzzleTrm;      // �ѱ� ��ġ

    public UnityEvent OnShoot;
    public UnityEvent OnStopShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent<int> OnChangeAmmo;

    protected bool isShooting = false;
    protected bool delayCoroutine = false;

    #region ���߿� SO �� �����ϴ� �κ�
    [SerializeField]
    private float weaponDelay = 0.25f, spreadAngle = 5f;        // + ź����
    [SerializeField]
    private bool isAutoFire = true;
    [SerializeField]
    private int maxAmmoCapacity = 40;
    [SerializeField]
    private int bulletCount = 1;    // �ѹ߾� ��� ��
    #endregion

    #region źȯ�� ���� �ڵ��
    protected int ammo;
    public int Ammo
    {
        get => ammo;
        set
        {
            ammo = Mathf.Clamp(value, 0, maxAmmoCapacity);
        }
    }
    public bool AmmoFull => Ammo == maxAmmoCapacity;
    public int EmptyBulletCount => maxAmmoCapacity - ammo;      // ���ε��� ������ źȯ��
    #endregion

    #region ���߿� Ǯ�Ŵ����� ó���� �κ�
    [SerializeField]
    private Bullet bulletPrefab;
    #endregion

    private void Start()
    {
        Ammo = maxAmmoCapacity;     // �ִ�ġ�� �־��ش�.
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if (isShooting && delayCoroutine == false)      // �߻� �� ������ ��ư�� ���� ����
        {
            // �Ѿ��ܷ�Ȯ��
            if (Ammo >= bulletCount)
            {
                OnShoot?.Invoke();  // �߻��߾�
                for (int i = 0; i < bulletCount; i++)
                {
                    ShootBullet();
                    Ammo--;
                    OnChangeAmmo?.Invoke(Ammo); // ���߿� UI
                }
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishOnShooting();     // �ܹ߰� ���縦 �����ϱ� ���ؼ�
        }
    }

    private void FinishOnShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());      // �ڵ��߻� üũ

        if (isAutoFire == false)
        {
            isShooting = false;
        }
    }

    private IEnumerator DelayNextShootCoroutine()
    {
        delayCoroutine = true;
        yield return new WaitForSeconds(weaponDelay);
        delayCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzleTrm.position);
    }

    private void SpawnBullet(Vector3 position)
    {
        Quaternion rotation = muzzleTrm.rotation;
        float spread = Random.Range(-spreadAngle, spreadAngle);
        rotation = rotation * Quaternion.Euler(0, 0, spread);

        Bullet b = Instantiate(bulletPrefab, position, rotation);     // ���� ȸ�������� �����ǿ� �Ѿ� ����
        b.IsEnemy = false;      // �� �Ѿ��� �ƴϴϱ�. �����ϱ�
    }

    public void TryShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
        OnStopShoot?.Invoke();
    }
}
