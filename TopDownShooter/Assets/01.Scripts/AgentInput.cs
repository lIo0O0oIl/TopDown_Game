using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPressed = null;
    public UnityEvent<Vector2> OnPointerPositionChanged = null;
    public UnityEvent OnFireButtonPressed = null;
    public UnityEvent OnFireButtonReleased = null;
    private bool fireButtonDown = false;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        GetKeyInput();
        GetPointerInput();
        GetFireInput();
    }

    private void GetFireInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (fireButtonDown == false)
            {
                fireButtonDown = true;
                OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if (fireButtonDown == true)
            {
                fireButtonDown = false;
                OnFireButtonReleased?.Invoke();
            }
        }
    }

    private void GetPointerInput()
    {
        // 스크린 좌표, 월드 좌표, 뷰포트좌표
        Vector3 mousePos = Input.mousePosition;     // 스크린 포지션의 마우스 좌표를 알아옴
        mousePos.z = 0;
        Vector2 mouseInWorldPos = mainCam.ScreenToWorldPoint(mousePos);     // 월드 좌표로 변경해줘

        OnPointerPositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetKeyInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // Raw가 있는 이유는 키보드로 입력을 받는 것임

        Vector2 dir = new Vector2(x, y);

        OnMovementKeyPressed?.Invoke(dir.normalized);
        // 노멀라이즈드는 길이를 1로 해주고 방향만 보여줌
    }
}
