using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "SO/BulletData")]
public class BulletDtatSO : ScriptableObject
{
    public int Damage = 1;
    public float BulletSpeed = 20;
    public ImpactScript ObstacleImpactPrefab;       // 장애물에 맞았을 때 나올 프리팹
    public ImpactScript EnemyImpactPrefab;       // 적에 맞았을 때 나올 프리팹

    public float LifeTime = 1.5f;
}
