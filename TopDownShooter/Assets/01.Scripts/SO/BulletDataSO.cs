using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "SO/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public int MinDamage = 1;
    public int MaxDamage = 5;
    public float BulletSpeed = 20;
    public ImpactScript ObstacleImpactPrefab;       // 장애물에 맞았을 때 나올 프리팹
    public ImpactScript EnemyImpactPrefab;       // 적에 맞았을 때 나올 프리팹

    public float LifeTime = 1.5f;
    public float KnockbackPower = 3f;       // 총알의 넉백 힘
    public float knockbackTime = 0.5f;
}
