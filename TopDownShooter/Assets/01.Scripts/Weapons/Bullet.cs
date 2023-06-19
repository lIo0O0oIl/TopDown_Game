using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    public bool IsEnemy;

    // SO 변경예정
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
        rigid.MovePosition(transform.position + transform.right * bulletData.BulletSpeed * Time.deltaTime);      // 바라보고 있는 오른쪽으로 이동

        if (timeToLive >= bulletData.LifeTime)     // 만약 타임투리브가 라이프 타임보다 크다면
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
            isDead = true;      // 이렇게 하는 이유는 1개 이상의 물체에 출돌처리가 되지 않도록 하는 것임
            PoolManager.Instance.Push(this);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnHitEnemy(collision);
            isDead = true;      // 이렇게 하는 이유는 1개 이상의 물체에 출돌처리가 되지 않도록 하는 것임
            PoolManager.Instance.Push(this);
        }

        // 여기에 스케일과 포지션 잡아주는 부분이 있어야 한다.

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

            int damage = Random.Range(bulletData.MinDamage, bulletData.MaxDamage + 1);

            PopupText txt = PoolManager.Instance.Pop("PopupText") as PopupText;

            Vector3 result = Vector3.Cross(Vector2.up, transform.right);

            float destXPos = result.z > 0 ? -5 : 5;     // 맞으면 왼쪽
            Vector3 startPos = point + new Vector3(0, 0.5f, 0);
            Vector3 endPos = point + new Vector3(destXPos, 0, 0);

            if (GameManager.Instance.CalcCriticalDamage(ref damage))
            {
                txt.SetUp(damage.ToString(), startPos, endPos, Color.red, 12f);
            }
            else
            {
                txt.SetUp(damage.ToString(), startPos, endPos, Color.white);
            }

            health.GetHit(damage, point, transform.right * -1);

            // 데미지띄워주기 TMP

            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;      // 저건 유닛이 있어서 길이 1짜리로 정규화 되어있음. 반지름이 1인 원 안의 좌표를 랜덤으로 줌

            ImpactScript impact = PoolManager.Instance.Pop(bulletData.EnemyImpactPrefab.name) as ImpactScript;       // 랜덤으로 생성하자
            impact.SetPositionAndRotation(point + (Vector3)randomOffset, rot);
        }

        if (col.TryGetComponent<AgentMovement>(out AgentMovement movement))
        {
            movement.Knockback(transform.right * bulletData.KnockbackPower, bulletData.knockbackTime);
        }
    }

}
