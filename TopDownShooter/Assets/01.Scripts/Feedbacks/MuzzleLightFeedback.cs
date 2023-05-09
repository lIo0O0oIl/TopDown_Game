using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MuzzleLightFeedback : Feedback
{
    [SerializeField]
    private Light2D lightTarget = null;
    [SerializeField]
    private float lightOnDelay = 0.01f, lightOffDelay = 0.01f;      // ����Ʈ�� �Ѱ� ������ ������ �ð�
    [SerializeField]
    private bool defaultState = false;      // �⺻ ���´� false;

    public override void CompleteFeedback()
    {
        StopAllCoroutines();
        lightTarget.enabled = defaultState;     // ����Ʈ ���ְ�
    }

    public override void CreateFeedback()
    {
        // ���� �����ٰ� ������ �ڵ�
        StartCoroutine(ToggleLightCoroutine(lightOnDelay, true, () =>
        {
            StartCoroutine(ToggleLightCoroutine(lightOffDelay, false));
        }));
    }

    IEnumerator ToggleLightCoroutine(float time, bool result, Action FinishCallback = null)
    {
        yield return new WaitForSeconds(time);
        lightTarget.enabled = result;
        FinishCallback?.Invoke();
    }
}
