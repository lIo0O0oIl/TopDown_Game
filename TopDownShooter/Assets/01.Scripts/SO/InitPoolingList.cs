using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolingPair
{
    public PoolableMono prefab;
    public int poolCount;
}

[CreateAssetMenu(menuName ="SO/poolList")]
public class InitPoolingList : ScriptableObject
{
    public List<PoolingPair> PoolingPair;
}
