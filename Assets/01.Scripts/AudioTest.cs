using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 10; i++)
        {
            GameManager.Instance.soundHandler.Play("LazerPistolShot");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
