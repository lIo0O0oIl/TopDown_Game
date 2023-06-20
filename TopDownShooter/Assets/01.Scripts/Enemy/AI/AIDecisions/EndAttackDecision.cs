using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttackDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return !actionData.IsAttack;        // 공격중일땐 false, 그렇지 않으면 true
    }
}
