using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBlink : Feedback
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float blinkTime = 0.1f;

    private readonly int isOutLineHash = Shader.PropertyToID("_IsOutLine");

    public override void CompleteFeedback()
    {
        StopAllCoroutines();
        spriteRenderer.material.SetInt(isOutLineHash, 0);       // 0ÀÌ false
    }

    public override void CreateFeedback()
    {
        if (spriteRenderer.material.HasProperty(isOutLineHash))
        {
            spriteRenderer.material.SetInt(isOutLineHash, 1);

            StartCoroutine(WaitBeforeChangeBack());
        }
    }

    private IEnumerator WaitBeforeChangeBack()
    {
        yield return new WaitForSeconds(blinkTime);
        spriteRenderer.material.SetInt(isOutLineHash, 0);
    }
}
