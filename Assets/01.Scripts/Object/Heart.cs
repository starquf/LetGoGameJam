using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IPoolableComponent
{
    public int healVal;

    public void Despawned()
    {

    }

    public void Spawned()
    {

    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);

        gameObject.SetActive(false);
    }
}
