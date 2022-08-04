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

        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Score, "5");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Score, "154");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Score, "154123");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Score, "23455233");
    }
}
