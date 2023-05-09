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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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

        ImpactScript inpact = Instantiate(bulletDtat.ObstacleImpactPrefab, transform.position, Quaternion.identity);

        // 여기에 스케일과 포지션 잡아주는 부분이 있어야 한다.

        Destroy(gameObject);    // 부딪히면 사먕
    }
}
