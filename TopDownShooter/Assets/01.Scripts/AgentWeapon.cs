using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    private float desireAngle;      // 무기가 바라보고자 하는 방향

    protected WeaponRenderer weaponRenderer;
    protected Weapon weapon;

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
    // 웨폰은 영구적인 것이 아니기 때문에 이렇게 만들어 주는 것임.
}
