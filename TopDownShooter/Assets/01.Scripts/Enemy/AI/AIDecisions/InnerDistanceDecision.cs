using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField]
    private float distance = 5f;

    public override bool MakeADecision()
    {
        // 단 이때 적의 베이그 포지션으로 변경을 해야한다.
        float distance = Vector2.Distance(enemyBrain.transform.position, enemyBrain.PlayerTrm.position);
        return distance < this.distance;
    }
}
