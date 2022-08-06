using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.mapMin = transform.GetChild(0).transform.position;
        GameManager.Instance.mapMax = transform.GetChild(1).transform.position;
    }
}
