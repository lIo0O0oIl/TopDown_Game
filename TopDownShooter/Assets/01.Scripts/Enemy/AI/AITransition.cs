using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    protected List<AIDecision> DecisionList = new List<AIDecision>();

    public AIState TargetState;     // ���� ������ ������ �������¸� ������ �־�� ��

    public void SetUp(Transform parentTrm)
    {
        GetComponents<AIDecision>(DecisionList);
        foreach(AIDecision dec in DecisionList)
        {
            // �� �������� �¾������ �Ѵ�.
            dec.SetUp(parentTrm);
        }
    }

    public bool CanTransition()     // ���̸� �� �� �ִ��� �˻��Ѵ�.
    {
        bool result = false;

        foreach(AIDecision dec in DecisionList)
        {
            result = dec.MakeADecision();       // ������ ����
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
