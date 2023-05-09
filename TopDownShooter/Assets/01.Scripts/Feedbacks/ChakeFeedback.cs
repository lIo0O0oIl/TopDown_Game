using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakeFeedback : Feedback
{
    [SerializeField]
    private Transform objectToShake;        // 흔들 물체
    [SerializeField]
    private float duration = 0.2f, strenght = 1f, randomness = 90f;
    [SerializeField]
    private int vibrato = 10;

    [SerializeField]
    private bool snapping = false, fadeOut = true;

    public override void CompleteFeedback()
    {
        objectToShake.DOComplete();
        // DoKill, DOComplete에서 킬은 하던걸 다 멈추는것, 컴플리트는 강제로 끝으로 보내는 것.
    }

    public override void CreateFeedback()
    {
        CompleteFeedback();     // 일단 하던거 끝으로 보내고
        objectToShake.DOShakePosition(duration, strenght, vibrato, randomness, snapping, fadeOut);
    }
}
