using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private float weaponDelay = 0.25f;
    [SerializeField]
    private bool isAutoFire = true;
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
        Debug.Log("빵야빵야");
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
