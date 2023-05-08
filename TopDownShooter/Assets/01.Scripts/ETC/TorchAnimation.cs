using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchAnimation : MonoBehaviour
{
    private Light2D _light;
    private float initIntensity;

    private void Awake()
    {
        _light = transform.Find("TorchLight").GetComponent<Light2D>();
        initIntensity = _light.intensity;
    }

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DOTween.To(getter: GetIntensity, setter: SetIntensity, endValue: 4f, duration: 1f)
                *//*.SetLoops(-1, LoopType.Yoyo)*//*.SetEase(Ease.OutSine);        //DOTween.To(겟터, 셋터, 값, 시간);
        }
    }*/

    private void OnEnable()
    {
        ShakeLight();
    }

    private float sign = 1;

    private void ShakeLight()
    {
        float offset = Random.Range(0, 0.5f);
        float endValue = initIntensity + offset * sign;
        float duration = Random.Range(0.4f, 1f);

        DOTween.To(getter: GetIntensity, setter: SetIntensity, endValue: endValue, duration: duration)
            .SetEase(Ease.OutSine)
            .OnComplete(ShakeLight);        //DOTween.To(겟터, 셋터, 값, 시간);

        sign *= -1;
    }

    private void SetIntensity(float value)
    {
        _light.intensity = value;
    }

    private float GetIntensity()
    {
        return _light.intensity;
    }
}
