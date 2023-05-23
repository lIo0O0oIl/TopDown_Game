using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    #region SO 로 대체될 예정
    [SerializeField]
    private float coolTime = 1f;    // 한번 공격 시 1 초 기다렸다가 다음 공격
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
