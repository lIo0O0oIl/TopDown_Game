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

    public override void Attack(int damage, Vector3 target)
    {
        Vector2 dir = target - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, range, whatIsEnemy);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
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
