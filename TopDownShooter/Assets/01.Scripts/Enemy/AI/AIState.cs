using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    protected List<AIAction> ActionList = new List<AIAction>();     // 내 상태에서 수행할 액션들
    protected List<AITransition> AItransitionList = new List<AITransition>();       // 내 상태에서 전이할 조건들

    protected EnemyBrain brain;

    public void SetUp(Transform parent)
    {
        brain = parent.GetComponent<EnemyBrain>();
        GetComponents<AIAction>(ActionList);
        ActionList.ForEach(a => a.SetUp(parent));

        GetComponentsInChildren<AITransition>(AItransitionList);
        AItransitionList.ForEach(t => t.SetUp(parent));
    }

    // 상태를 계속 업데이트 해준다.
    public void UpdateState()
    {
        // 포문으로 해도 가능
        foreach(AIAction action in ActionList)
        {
            action.TakeAction();        // 내가 가진 모든 액션을 전부 실행해준다.
        }

        //ActionList.ForEach(a => a.TakeAction());  느리나 간편

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
