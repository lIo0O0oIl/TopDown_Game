using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse = false;      // 조건을 역으로 검사할건지
    protected AIActionData actionData;
    protected EnemyBrain enemyBrain;

    public virtual void SetUp(Transform parentTrm)
    {
        actionData = parentTrm.Find("AI").GetComponent<AIActionData>();
        enemyBrain = parentTrm.GetComponent<EnemyBrain>();
    }

    // 조건 결정 결과를 true, false 로 알려준다.
    public abstract bool MakeADecision();
}
