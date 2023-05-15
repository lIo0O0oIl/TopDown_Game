using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimator : MonoBehaviour
{
    protected Animator animator;
    protected readonly int walkBoolHash = Animator.StringToHash("Walk");
    protected readonly int deathTriggerHash = Animator.StringToHash("Death");

    public UnityEvent OnFootStep = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DeathTrigger(bool value)
    {
        if (value)
        {
            animator.SetTrigger(deathTriggerHash);
        }
        else
        {
            animator.ResetTrigger(deathTriggerHash);
        }
    }

    public void AnimationPlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    private void SetWalkAnimation(bool value)
    {
        animator.SetBool(walkBoolHash, value);
    }

    public void FootStepEvent()
    {
        OnFootStep?.Invoke();
    }
}
