using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    private float desireAngle;      // 무기가 바라보고자 하는 방향

    protected WeaponRenderer weaponRenderer;
    protected Weapon weapon;

    #region 탄환관련 로직
    public UnityEvent<int, int> OnChangeTotalAmmo;
    [SerializeField]
    private int mazTotalAmmo = 9999, totalAmmo = 300;       // 최대 9999발, 지금은 300발

    public int TotalAmmo
    {
        get => totalAmmo;
        set
        {
            totalAmmo = Mathf.Clamp(value, 0, totalAmmo);
            OnChangeTotalAmmo?.Invoke(weapon.Ammo, totalAmmo);
        }
    }
    #endregion

    #region 재장전 로직
    private bool isReloading = false;
    [SerializeField]
    private ReloadBar reloadBar;

    public void Reload()
    {
        if (isReloading == false && totalAmmo > 0 && weapon.AmmoFull == false)
        {
            isReloading = true;
            weapon.StopShooting();
            StartCoroutine(ReloadCoroutine());
        }
        else
        {
            Debug.Log("재장전 불가");
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        reloadBar.gameObject.SetActive(true);
        float time = 0;
        float reloadTime = 1f;
        while(time <= reloadTime)
        {
            time += Time.deltaTime;
            reloadBar.ReloadBaugeNormal(time / reloadTime);
            yield return null;
        }
        reloadBar.gameObject.SetActive(false);

        int reloadedAmmo = Mathf.Min(totalAmmo, weapon.EmptyBulletCount);
        totalAmmo -= reloadedAmmo;
        weapon.Ammo += reloadedAmmo;

        weapon.PlayReloadSound();       // 리로드 사운드 재생
        OnChangeTotalAmmo?.Invoke(weapon.Ammo, totalAmmo);
        isReloading = false;
    }
    #endregion

    protected void Awake()
    {
        weaponRenderer = transform.Find("AssaultRifle").GetComponent<WeaponRenderer>();
        weapon = transform.Find("AssaultRifle").GetComponent<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPosition)
    {
        Vector3 aimDirection = (Vector3)pointerPosition - transform.position;

        desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(desireAngle, Vector3.forward);
        // = Quaternion.Euler(0, 0, desireAngle);
    }

    private void AdjustWeaponRendering()
    {
        if (weaponRenderer != null)
        {
            weaponRenderer.FlipSprite(desireAngle > 90f || desireAngle < -90f);
            weaponRenderer.RenderBehindHead(desireAngle > 0f && desireAngle < 180f);
        }
    }

    public void Shoot()
    {
        if (isReloading == false)
        {
            weapon?.TryShooting();
        }
    }

    public void StopShooting()
    {
        weapon?.StopShooting();
    }
    // 웨폰은 영구적인 것이 아니기 때문에 이렇게 만들어 주는 것임.
}
