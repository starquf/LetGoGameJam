using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.1f);
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Ammo, "5");
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Level, "154");
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Score, "154154164");
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Exp, ".5");
    }
}
