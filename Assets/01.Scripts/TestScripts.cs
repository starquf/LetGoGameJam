using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.soundHandler.Play("Test");
    }
}
