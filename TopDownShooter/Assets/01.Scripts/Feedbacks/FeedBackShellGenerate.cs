using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackShellGenerate : Feedback
{
    [SerializeField]
    private Transform shellPosition;

    public override void CompleteFeedback()
    {

    }

    public override void CreateFeedback()
    {
        Vector3 shellPos = shellPosition.position;
        float upDriection = shellPosition.parent.localScale.y > 0 ? -1 : 1;
        Vector3 direction = shellPosition.up * upDriection + shellPosition.forward * -0.5f;

        TextureParticleManager.Instance.SpawnShell(shellPos, direction);
    }
}
