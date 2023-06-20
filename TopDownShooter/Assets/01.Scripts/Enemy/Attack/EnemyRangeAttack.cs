using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField]
    private FireBall bulletPrefab;        // ����ü ������

    [SerializeField]
    private float coolTime = 3f;

    private float lastFireTime = 0;     // ������ �߻�ð�
    private AIActionData actionData;

    private FireBall currentFireBall;

    private void Awake()
    {
        actionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public override void Attack(Vector3 target)
    {
        if (actionData.IsAttack == false && lastFireTime + coolTime < Time.time)
        {
            actionData.IsAttack = true;
            // ���⼭ ���� ������ ������
            AttackSequence();
        }
    }

    private void AttackSequence()
    {
        currentFireBall = PoolManager.Instance.Pop(bulletPrefab.name) as FireBall;
        currentFireBall.transform.position = transform.position + new Vector3(0, 0.25f, 0);
        currentFireBall.transform.localScale = Vector3.one * 0.1f;      // 1/10 ũŰ�� ���´�.

        Sequence seq = DOTween.Sequence();
        seq.Append(currentFireBall.transform.DOMoveY(currentFireBall.transform.position.y + 1f, 0.5f));
        seq.Join(currentFireBall.transform.DOScale(Vector3.one * 0.4f, 0.5f));

        seq.Append(currentFireBall.transform.DOScale(Vector3.one, 1.2f));       // 1�� ũ�⸦ Ű���

        Tween t = DOTween.To(() =>              // ����, ����, ��ǥ��, �ð��̷��� �������� ����
            currentFireBall.Light.intensity,
            value => currentFireBall.Light.intensity = value,
            currentFireBall.LightMaxIntensity,
            1.2f
        );

        seq.Join(t);

        seq.AppendCallback(() =>
        {
            lastFireTime = Time.time;
            actionData.IsAttack = false;
            currentFireBall.Fire(currentFireBall.transform.right);
            currentFireBall = null;
        });
    }

    public void FaceDirection(Vector2 pointerTarget)
    {
        if (currentFireBall == null) return;
        // ���⼭ �� ������ �ٶ󺸵��� ����

        Vector3 dir = (Vector3)pointerTarget - currentFireBall.transform.position;

        // �������ϱ�
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        currentFireBall.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void CancelAttack()
    {

    }

    public override void PreAttack()
    {

    }
}
