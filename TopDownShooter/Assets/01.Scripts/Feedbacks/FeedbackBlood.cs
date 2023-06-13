using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBlood : Feedback
{
    [SerializeField]
    private AIActionData actionData;
    [SerializeField]
    [Range(0, 1f)]
    private float sizeFactor;

    public override void CompleteFeedback()
    {

    }

    public override void CreateFeedback()
    {
        TextureParticleManager.Instance.SpawnBlood(actionData.HitPoint, actionData.HitNormal, sizeFactor);
    }
}
