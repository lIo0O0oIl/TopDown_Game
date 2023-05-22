using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : AgentRenderer
{


    public void ShowProcess(float time, Action Callback)
    {

    }

    private IEnumerator ShowCoroutine(float time, Action Callback)
    {
        Material mat = spriteRenderer.material;
        yield return null;
    }
}
