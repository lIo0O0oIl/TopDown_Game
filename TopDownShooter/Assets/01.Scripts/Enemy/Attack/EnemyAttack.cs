using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public abstract void Attack(int damage, Vector3 target);
    public abstract void PreAttack();
    public abstract void CancelAttack();
}
