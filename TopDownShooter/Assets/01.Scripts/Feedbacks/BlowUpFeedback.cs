using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpFeedback : Feedback
{
    [SerializeField]
    private EnemyBrain brain;
    [SerializeField]
    private AIActionData actionData;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private AgentMovement agentMovement;

    [SerializeField]
    private Color deathColor;

    private Sequence tweenSeq;

    [SerializeField]
    private float blowPower = 5f;

    public override void CompleteFeedback()
    {
        if (tweenSeq != null && tweenSeq.IsActive())    // 아직 시퀀스가 살아있어
        {
            tweenSeq.Kill();
        }
    }

    public override void CreateFeedback()
    {
        CompleteFeedback();
        brain.AgentAnimatorCompo.SetAnimationSpeed(0f);      // 날라가기전에 애니메이션 정지시키고
        Vector2 direction = actionData.HitNormal * -1 * blowPower;

        float rotate = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        agentMovement.Knockback(direction, 0.5f, true);

        tweenSeq = DOTween.Sequence();
        tweenSeq.Append(brain.transform.DORotate(new Vector3(0, 0, rotate), 0.5f));
        tweenSeq.Join(spriteRenderer.DOColor(deathColor, 0.6f));
    }
}
