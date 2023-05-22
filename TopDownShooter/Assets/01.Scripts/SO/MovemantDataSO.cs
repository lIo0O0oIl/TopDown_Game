using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Agent/MovementData")]
public class MovemantDataSO : ScriptableObject
{
    [Range(1, 10)]
    public float MaxSpeed;

    [Range(0.1f, 100f)]
    public float Accle = 50, DeAccle = 50;
}
