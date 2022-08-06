using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IPoolableComponent
{
    public float addAmmoPersent = 5f;

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
