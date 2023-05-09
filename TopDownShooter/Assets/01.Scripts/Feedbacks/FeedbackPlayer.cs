using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> feedbackToPlay = new List<Feedback>();

    private void Awake()
    {
        GetComponents<Feedback>(feedbackToPlay);
    }

    public void PlayFeedback()
    {
        CompleteFeedback();
        foreach(Feedback f in feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }

    public void CompleteFeedback()
    {
        foreach (Feedback f in feedbackToPlay)
        {
            f.CompleteFeedback();
        }
    }
}
