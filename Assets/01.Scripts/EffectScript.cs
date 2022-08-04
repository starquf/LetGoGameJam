using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public float dur;

    private WaitForSeconds spawnWait = null;

    private void Start()
    {
        spawnWait = new WaitForSeconds(dur);

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return spawnWait;

        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
