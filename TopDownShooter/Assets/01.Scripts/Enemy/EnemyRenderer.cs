using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRenderer : AgentRenderer
{
    protected Animator animator;

    private readonly int showRateHash = Shader.PropertyToID("_ShowRate");
    private readonly int dissolveHash = Shader.PropertyToID("_Dissolve");
    private readonly int isDissolveHash = Shader.PropertyToID("_IsDissolve");

    public UnityEvent OnDissolveEnd;

    [SerializeField]
    private float yOffset = 0.6f;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public void Init()
    {
        Material mat = spriteRenderer.material;
        mat.SetInt(isDissolveHash, 0);
        mat.SetFloat(showRateHash, -1f);
        transform.rotation = Quaternion.identity;
    }

    public void StartDissolve(float time)
    {
        StartCoroutine(DissolveCoroutine(time));
    }

    private IEnumerator DissolveCoroutine(float time)
    {
        Material mat = spriteRenderer.material;

        float percent = 0;
        float currentTime = 0;
        mat.SetInt(isDissolveHash, 1);
        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            float value = Mathf.Lerp(1, 0, percent);

            mat.SetFloat(dissolveHash, value);
            yield return null;
        }

        OnDissolveEnd?.Invoke();
    }

    public void ShowProcess(float time, Action Callback)
    {
        StartCoroutine(ShowCoroutine(time, Callback));
    }

    private IEnumerator ShowCoroutine(float time, Action Callback)
    {
        Material mat = spriteRenderer.material;

        float currentRate = 1f;
        float percent = 0;
        float currentTime = 0;

        float currentOffset = -yOffset;
        transform.localPosition = new Vector3(0, -yOffset, 0);

        animator.speed = 0;
        while(percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            currentRate = Mathf.Lerp(1, -1, percent);
            currentOffset = Mathf.Lerp(-yOffset, 0, percent);

            transform.localPosition = new Vector3(0, currentOffset, 0);
            mat.SetFloat(showRateHash, currentRate);
            yield return null;
        }

        animator.speed = 1;

        Callback?.Invoke();
    }
}
