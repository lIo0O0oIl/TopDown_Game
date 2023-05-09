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
    private float lightOnDelay = 0.01f, lightOffDelay = 0.01f;      // 라이트가 켜고 꺼짐의 딜레이 시간
    [SerializeField]
    private bool defaultState = false;      // 기본 상태는 false;

    public override void CompleteFeedback()
    {
        StopAllCoroutines();
        lightTarget.enabled = defaultState;     // 라이트 꺼주고
    }

    public override void CreateFeedback()
    {
        // 빛이 켜졌다가 꺼지는 코드
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
