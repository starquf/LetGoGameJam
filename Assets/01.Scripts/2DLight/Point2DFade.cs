using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class Point2DFade : MonoBehaviour
{
    [SerializeField]
    private Light2D point2D;

    [SerializeField]
    private float cycleTime = 1f;

    [SerializeField]
    private float minIntensity = 0.3f;
    [SerializeField]
    private float maxIntensity = 0.6f;

    [SerializeField]
    private float changeAmount;

    private Sequence seq;

    private bool isAdd = true;

    private void Awake()
    {
        point2D = GetComponent<Light2D>();
        point2D.intensity = minIntensity;

        changeAmount = (maxIntensity - minIntensity) / (cycleTime / 2);
    }

    private void Update()
    {
        if(isAdd)
        {
            point2D.intensity += changeAmount * Time.deltaTime;
            if (point2D.intensity >= maxIntensity)
            {
                isAdd = false;
            }
        }
        else
        {
            point2D.intensity -= changeAmount * Time.deltaTime;
            if (point2D.intensity <= minIntensity)
            {
                isAdd = true;
            }
        }
    }
}
