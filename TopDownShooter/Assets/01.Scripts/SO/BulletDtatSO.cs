using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "SO/BulletData")]
public class BulletDtatSO : ScriptableObject
{
    public int Damage = 1;
    public float BulletSpeed = 20;
    public ImpactScript ObstacleImpactPrefab;       // ��ֹ��� �¾��� �� ���� ������
    public ImpactScript EnemyImpactPrefab;       // ���� �¾��� �� ���� ������

    public float LifeTime = 1.5f;
}
