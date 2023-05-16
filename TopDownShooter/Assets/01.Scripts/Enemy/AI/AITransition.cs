using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    protected List<AIDecision> DecisionList = new List<AIDecision>();

    public AIState TargetState;     // 어디로 전이할 것인지 다음상태를 가지고 있어야 해

    public void SetUp(Transform parentTrm)
    {
        GetComponents<AIDecision>(DecisionList);
        foreach(AIDecision dec in DecisionList)
        {
            // 각 결정들을 셋업해줘야 한다.
            dec.SetUp(parentTrm);
        }
    }

    public bool CanTransition()     // 전이를 할 수 있는지 검사한다.
    {
        bool result = false;

        foreach(AIDecision dec in DecisionList)
        {
            result = dec.MakeADecision();       // 결정을 내려
            if (dec.IsReverse)
            {
                result = !result;
            }
            if (result == false)
            {
                break;
            }
        }

        return result;
    }
}
