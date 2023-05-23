using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    #region SO �� ��ü�� ����
    [SerializeField]
    private float coolTime = 1f;    // �ѹ� ���� �� 1 �� ��ٷȴٰ� ���� ����
    [SerializeField]
    private int damage = 2;
    #endregion

    private float lastAtkTime;

    public override void TakeAction()
    {
        enemyBrain.Move(Vector2.zero, transform.position);

        if (lastAtkTime +coolTime < Time.time)
        {
            enemyBrain.Attack(damage, enemyBrain.PlayerTrm.position);
            lastAtkTime = Time.time;
            actionData.IsAttack = true;
        }
    }
}
