using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionUpdater : MonoBehaviour
{
    public static FunctionUpdater Instance;

    private Action updateAction;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        updateAction?.Invoke();
    }

    public void Create(Action act)
    {
        updateAction += act;
    }
}
