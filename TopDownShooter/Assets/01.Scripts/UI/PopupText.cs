using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : PoolableMono
{
    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void SetUp(string text, Vector3 pos, Vector3 targetPos, Color color, float fontSize = 7f)
    {
        transform.position = pos;
        textMesh.SetText(text);
        textMesh.color = color;
        textMesh.fontSize = fontSize;

        ShowSequence(1.5f, targetPos);
    }

    private void ShowSequence(float time, Vector3 targetPos)
    {
        Sequence seq = DOTween.Sequence();

        float jumpPower = 2f;
        seq.Append(transform.DOJump(targetPos, jumpPower, 1, time));       // 올라가면서
        seq.Join(textMesh.DOFade(0, time));     // 페이드 아웃
        seq.AppendCallback(() =>        // 다 사라지면 넣어주기
        {
            PoolManager.Instance.Push(this);
        });
    }

    public override void Init()
    {
        textMesh.alpha = 1f;        // 페이드 아웃 시켰던 것 리셋

    }
}
