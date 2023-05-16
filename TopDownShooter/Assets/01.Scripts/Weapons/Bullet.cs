using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsEnemy;

    // SO ���濹��
    [SerializeField]
    private BulletDtatSO bulletDtat;

    private float timeToLive = 0;

    private Rigidbody2D rigid;
    private bool isDead = false;

    private float radius;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        radius = GetComponent<CapsuleCollider2D>().size.y * 0.5f;
    }

    private void FixedUpdate()
    {
        timeToLive += Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + transform.right * bulletDtat.BulletSpeed * Time.deltaTime);      // �ٶ󺸰� �ִ� ���������� �̵�

        if (timeToLive >= bulletDtat.LifeTime)     // ���� Ÿ�������갡 ������ Ÿ�Ӻ��� ũ�ٸ�
        {
            isDead = true;
            Destroy(gameObject);        // ���߿� Ǯ�Ŵ����� �ٲ����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        isDead = true;      // �̷��� �ϴ� ������ 1�� �̻��� ��ü�� �⵹ó���� ���� �ʵ��� �ϴ� ����

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            OnHitObstacle();
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnHitEnemy();
            Destroy(gameObject);
        }

        // ���⿡ �����ϰ� ������ ����ִ� �κ��� �־�� �Ѵ�.

        Destroy(gameObject);    // �ε����� ���
    }

    private void OnHitObstacle()
    {
        ImpactScript impact = Instantiate(bulletDtat.ObstacleImpactPrefab, transform.position, Quaternion.identity);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 5f, 1 << LayerMask.NameToLayer("Obstacle"));

        if (hit.collider != null)
        {
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            impact.SetPositionAndRotation(hit.point + (Vector2)transform.right * 0.5f, rot);
        }
    }

    private void OnHitEnemy()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, transform.right, 5f, 1 << LayerMask.NameToLayer("Enemy"));
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
                health.GetHit(bulletDtat.Damage, hit.point, hit.normal);

                Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
                Vector2 randomOffset = Random.insideUnitCircle * 0.5f;      // ���� ������ �־ ���� 1¥���� ����ȭ �Ǿ�����. �������� 1�� �� ���� ��ǥ�� �������� ��

                ImpactScript impact = Instantiate(bulletDtat.EnemyImpactPrefab, transform.position, Quaternion.identity);       // �������� �����̴ϱ� �������� ��������!
                impact.SetPositionAndRotation(hit.point + randomOffset, rot);
            }
        }
    }
}
