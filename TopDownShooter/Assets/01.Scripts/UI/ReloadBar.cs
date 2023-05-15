using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    [SerializeField]
    private Transform barTrm;

    public void ReloadBaugeNormal(float value)
    {
        barTrm.transform.localScale = new Vector3(value, 1, 1);
    }
}
