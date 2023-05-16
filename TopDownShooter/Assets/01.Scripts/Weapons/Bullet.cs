using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsEnemy;

    // SO 변경예정
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
        rigid.MovePosition(transform.position + transform.right * bulletDtat.BulletSpeed * Time.deltaTime);      // 바라보고 있는 오른쪽으로 이동

        if (timeToLive >= bulletDtat.LifeTime)     // 만약 타임투리브가 라이프 타임보다 크다면
        {
            isDead = true;
            Destroy(gameObject);        // 나중에 풀매니저로 바꿔야해
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        isDead = true;      // 이렇게 하는 이유는 1개 이상의 물체에 출돌처리가 되지 않도록 하는 것임

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

        // 여기에 스케일과 포지션 잡아주는 부분이 있어야 한다.

        Destroy(gameObject);    // 부딪히면 사먕
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
                Vector2 randomOffset = Random.insideUnitCircle * 0.5f;      // 저건 유닛이 있어서 길이 1짜리로 정규화 되어있음. 반지름이 1인 원 안의 좌표를 랜덤으로 줌

                ImpactScript impact = Instantiate(bulletDtat.EnemyImpactPrefab, transform.position, Quaternion.identity);       // 같은곳은 노잼이니까 랜덤으로 생성하자!
                impact.SetPositionAndRotation(hit.point + randomOffset, rot);
            }
        }
    }
}
