using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMeleeAttack : EnemyAttack
{
    [SerializeField]
    private float range = 1;
    public UnityEvent OnAttackPress;

    [SerializeField]
    private LayerMask whatIsEnemy;
    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private float coolTime = 1f;
    private float lastAttackTime = 0;

    public override void Attack(Vector3 target)
    {
        if (lastAttackTime + coolTime < Time.time) return;

        Vector2 dir = target - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, range, whatIsEnemy);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
                lastAttackTime = Time.time;
                health.GetHit(damage, hit.point, hit.normal);
            }
        }

        OnAttackPress?.Invoke();
    }

    public override void CancelAttack()
    {

    }

    public override void PreAttack()
    {

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
            Gizmos.color = Color.white;
        }
    }
#endif
}
