using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.map = this;

        GameManager.Instance.mapMin = transform.GetChild(0).transform;
        GameManager.Instance.mapMax = transform.GetChild(1).transform;
    }

    public void SetMapScale(float scale, Action onEndScale = null)
    {
        transform.DOScale(Vector3.one * scale, 5f).OnComplete(() => onEndScale?.Invoke());
    }
}
