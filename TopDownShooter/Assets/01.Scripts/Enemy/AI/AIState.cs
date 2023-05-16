using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    protected List<AIAction> ActionList = new List<AIAction>();     // �� ���¿��� ������ �׼ǵ�
    protected List<AITransition> AItransitionList = new List<AITransition>();       // �� ���¿��� ������ ���ǵ�

    protected EnemyBrain brain;

    public void SetUp(Transform parent)
    {
        brain = parent.GetComponent<EnemyBrain>();
        GetComponents<AIAction>(ActionList);
        GetComponentsInChildren<AITransition>(AItransitionList);
    }

    // ���¸� ��� ������Ʈ ���ش�.
    public void UpdateState()
    {
        // �������� �ص� ����
        foreach(AIAction action in ActionList)
        {
            action.TakeAction();        // ���� ���� ��� �׼��� ���� �������ش�.
        }

        //ActionList.ForEach(a => a.TakeAction());  ������ ����

        foreach(AITransition tr in AItransitionList)
        {
            if (tr.CanTransition())
            {
                brain.ChangeState(tr.TargetState);
                break;
            }
        }
    }
}
