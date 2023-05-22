using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField]
    private float distance = 5f;

    public override bool MakeADecision()
    {
        // �� �̶� ���� ���̽� ���������� ������ �ؾ��Ѵ�.
        float distance = Vector2.Distance(enemyBrain.transform.position, enemyBrain.PlayerTrm.position);
        if (distance < this.distance)
        {
            actionData.LastSpotPosition = enemyBrain.PlayerTrm.position;
            return true;
        }

        return false;
    }
}
