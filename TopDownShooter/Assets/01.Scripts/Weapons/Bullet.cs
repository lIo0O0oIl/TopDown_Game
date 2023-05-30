using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    public bool IsEnemy;

    // SO ���濹��
    [SerializeField]
    private BulletDataSO bulletData;

    private float timeToLive = 0;

    private Rigidbody2D rigid;
    private bool isDead = false;

    //private float radius;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //radius = GetComponent<CapsuleCollider2D>().size.y * 0.5f;
    }

    public override void Init()
    {
        timeToLive = 0;
        isDead = false;
    }

    private void FixedUpdate()
    {
        timeToLive += Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + transform.right * bulletData.BulletSpeed * Time.deltaTime);      // �ٶ󺸰� �ִ� ���������� �̵�

        if (timeToLive >= bulletData.LifeTime)     // ���� Ÿ�������갡 ������ Ÿ�Ӻ��� ũ�ٸ�
        {
            isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            OnHitObstacle();
            isDead = true;      // �̷��� �ϴ� ������ 1�� �̻��� ��ü�� �⵹ó���� ���� �ʵ��� �ϴ� ����
            PoolManager.Instance.Push(this);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnHitEnemy(collision);
            isDead = true;      // �̷��� �ϴ� ������ 1�� �̻��� ��ü�� �⵹ó���� ���� �ʵ��� �ϴ� ����
            PoolManager.Instance.Push(this);
        }

        // ���⿡ �����ϰ� ������ ����ִ� �κ��� �־�� �Ѵ�.

    }

    private void OnHitObstacle()
    {
        ImpactScript impact = PoolManager.Instance.Pop(bulletData.ObstacleImpactPrefab.name) as ImpactScript;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 5f, 1 << LayerMask.NameToLayer("Obstacle"));

        if (hit.collider != null)
        {
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            impact.SetPositionAndRotation(hit.point + (Vector2)transform.right * 0.5f, rot);
        }
    }

    private void OnHitEnemy(Collider2D col)
    {
        if (col.TryGetComponent<IDamageable>(out IDamageable health))
        {
            Vector3 point = col.transform.position;

            health.GetHit(bulletData.Damage, point, transform.right * -1);

            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;      // ���� ������ �־ ���� 1¥���� ����ȭ �Ǿ�����. �������� 1�� �� ���� ��ǥ�� �������� ��

            ImpactScript impact = PoolManager.Instance.Pop(bulletData.EnemyImpactPrefab.name) as ImpactScript;       // �������� ��������
            impact.SetPositionAndRotation(point + (Vector3)randomOffset, rot);
        }

        if (col.TryGetComponent<AgentMovement>(out AgentMovement movement))
        {
            movement.Knockback(transform.right * bulletData.KnockbackPower, bulletData.knockbackTime);
        }
    }

}
