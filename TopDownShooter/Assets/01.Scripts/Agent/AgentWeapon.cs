using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    private float desireAngle;      // ���Ⱑ �ٶ󺸰��� �ϴ� ����

    protected WeaponRenderer weaponRenderer;
    protected Weapon weapon;

    #region źȯ���� ����
    public UnityEvent<int, int> OnChangeTotalAmmo;
    [SerializeField]
    private int mazTotalAmmo = 9999, totalAmmo = 300;       // �ִ� 9999��, ������ 300��

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
        weapon?.TryShooting();
    }

    public void StopShooting()
    {
        weapon?.StopShooting();
    }
    // ������ �������� ���� �ƴϱ� ������ �̷��� ����� �ִ� ����.
}
