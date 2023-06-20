using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        enemyBrain.Move(Vector2.zero, enemyBrain.PlayerTrm.position);
        enemyBrain.Attack(enemyBrain.PlayerTrm.position);
    }
}
