using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VirtualCameraScript : MonoBehaviour
{
    private Tween camTween = null;

    private void Awake()
    {
        GameManager.Instance.cmPerlinObject = GetComponent<CinemachineVirtualCamera>();
        GameManager.Instance.vCamScript = this;
    }


    public void Shake(BulletSO bulletData)
    {
        if (camTween != null)
            camTween.Kill();

        CinemachineBasicMultiChannelPerlin perlin = GameManager.Instance.cmPerlinObject.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin != null)
        {
            perlin.m_AmplitudeGain = bulletData.shakeAmount;

            camTween = DOTween.To(() => perlin.m_AmplitudeGain, value => perlin.m_AmplitudeGain = value, 0, bulletData.shakeTime);
        }
    }

    public void KillShake()
    {
        camTween.Kill();
    }
}
