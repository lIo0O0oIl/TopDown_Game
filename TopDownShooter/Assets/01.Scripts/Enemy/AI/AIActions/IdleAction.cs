using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        enemyBrain.Move(Vector2.zero, transform.position);  // 자기자신을 바라보도록 해서 정지 상태
    }
}
