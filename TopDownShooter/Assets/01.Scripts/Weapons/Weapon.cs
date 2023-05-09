using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected Transform muzzleTrm;      // 총구 위치

    public UnityEvent OnShoot;
    public UnityEvent OnStopShoot;

    protected bool isShooting = false;
    protected bool delayCoroutine = false;

    #region 나중에 SO 로 빼야하는 부분
    [SerializeField]
    private float weaponDelay = 0.25f, spreadAngle = 5f;        // + 탄퍼짐
    [SerializeField]
    private bool isAutoFire = true;
    #endregion

    #region 나중에 풀매니저로 처리할 부분
    [SerializeField]
    private Bullet bulletPrefab;
    #endregion

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if (isShooting && delayCoroutine == false)      // 발사 쿨도 비어었고 버튼도 눌린 상태
        {
            // 여기서 총알의 잔량도 확인하고 발사하지만 지금은 아님

            OnShoot?.Invoke();  // 발사했어
            ShootBullet();

            FinishOnShooting();     // 단발과 연사를 구분하기 위해서
        }
    }

    private void FinishOnShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());      // 자동발사 체크

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

        Bullet b = Instantiate(bulletPrefab, position, rotation);     // 머즐 회전량으로 포지션에 총알 생성
        b.IsEnemy = false;      // 적 총알이 아니니까. 내꺼니까
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
