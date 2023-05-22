using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField]
    private float distance = 5f;

    public override bool MakeADecision()
    {
        // 단 이때 적의 베이스 포지션으로 변경을 해야한다.
        float distance = Vector2.Distance(enemyBrain.transform.position, enemyBrain.PlayerTrm.position);
        if (distance < this.distance)
        {
            actionData.LastSpotPosition = enemyBrain.PlayerTrm.position;
            return true;
        }

        return false;
    }
}
