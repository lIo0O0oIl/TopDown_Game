using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakeFeedback : Feedback
{
    [SerializeField]
    private Transform objectToShake;        // ��� ��ü
    [SerializeField]
    private float duration = 0.2f, strenght = 1f, randomness = 90f;
    [SerializeField]
    private int vibrato = 10;

    [SerializeField]
    private bool snapping = false, fadeOut = true;

    public override void CompleteFeedback()
    {
        objectToShake.DOComplete();
        // DoKill, DOComplete���� ų�� �ϴ��� �� ���ߴ°�, ���ø�Ʈ�� ������ ������ ������ ��.
    }

    public override void CreateFeedback()
    {
        CompleteFeedback();     // �ϴ� �ϴ��� ������ ������
        objectToShake.DOShakePosition(duration, strenght, vibrato, randomness, snapping, fadeOut);
    }
}
