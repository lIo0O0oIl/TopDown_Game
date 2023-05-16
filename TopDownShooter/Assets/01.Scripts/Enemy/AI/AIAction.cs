using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData actionData;
    protected EnemyBrain enemyBrain;

    public virtual void SetUp(Transform parentTrm)
    {
        actionData = parentTrm.Find("AI").GetComponent<AIActionData>();
        enemyBrain = parentTrm.GetComponent<EnemyBrain>();
    }

    public abstract void TakeAction();
}
