using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.cmPerlinObject = GetComponent<CinemachineVirtualCamera>();
    }
}
