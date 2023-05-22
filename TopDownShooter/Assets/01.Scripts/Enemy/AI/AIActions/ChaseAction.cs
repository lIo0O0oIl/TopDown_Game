using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 dir = actionData.LastSpotPosition - enemyBrain.transform.position;

        enemyBrain.Move(moveDirection:dir.normalized, targetPositoin:actionData.LastSpotPosition);
    }
}
