using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsArriveDecision : AIDecision
{
    public override bool MakeADecision()
    {
        actionData.IsArrived = Vector2.Distance(actionData.LastSpotPosition, enemyBrain.transform.position) <= 0.3f;
        return actionData.IsArrived;
    }
}
