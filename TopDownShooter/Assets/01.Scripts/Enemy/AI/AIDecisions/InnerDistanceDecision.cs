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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distance);
            Gizmos.color = Color.white;
        }
    }
#endif
}
