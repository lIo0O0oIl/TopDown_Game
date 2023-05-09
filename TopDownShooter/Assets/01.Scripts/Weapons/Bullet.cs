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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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

        ImpactScript inpact = Instantiate(bulletDtat.ObstacleImpactPrefab, transform.position, Quaternion.identity);

        // ���⿡ �����ϰ� ������ ����ִ� �κ��� �־�� �Ѵ�.

        Destroy(gameObject);    // �ε����� ���
    }
}
