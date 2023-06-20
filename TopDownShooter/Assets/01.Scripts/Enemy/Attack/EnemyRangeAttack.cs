using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField]
    private FireBall bulletPrefab;        // 투사체 프리텝

    [SerializeField]
    private float coolTime = 3f;

    private float lastFireTime = 0;     // 마지막 발사시간
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
            // 여기서 공격 시퀀스 시작함
            AttackSequence();
        }
    }

    private void AttackSequence()
    {
        currentFireBall = PoolManager.Instance.Pop(bulletPrefab.name) as FireBall;
        currentFireBall.transform.position = transform.position + new Vector3(0, 0.25f, 0);
        currentFireBall.transform.localScale = Vector3.one * 0.1f;      // 1/10 크키로 놓는다.

        Sequence seq = DOTween.Sequence();
        seq.Append(currentFireBall.transform.DOMoveY(currentFireBall.transform.position.y + 1f, 0.5f));
        seq.Join(currentFireBall.transform.DOScale(Vector3.one * 0.4f, 0.5f));

        seq.Append(currentFireBall.transform.DOScale(Vector3.one, 1.2f));       // 1로 크기를 키우고

        Tween t = DOTween.To(() =>              // 겟터, 셋터, 목표값, 시간이렇게 가져오는 것임
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
        // 여기서 저 방향을 바라보도록 해줌

        Vector3 dir = (Vector3)pointerTarget - currentFireBall.transform.position;

        // 각도구하기
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
