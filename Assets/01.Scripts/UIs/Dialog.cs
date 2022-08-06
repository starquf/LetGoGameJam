using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour, IPoolableComponent
{
    [SerializeField]
    public Text dialogText;

    private Coroutine textPostionRoutine;

    public void Despawned()
    {
        
    }

    public void Spawned()
    {
        transform.localScale = Vector3.one;
    }

    public void SetPosition()
    {
        textPostionRoutine = StartCoroutine(PositionRoutine());
    }

    private IEnumerator PositionRoutine()
    {
        yield return new WaitForSeconds(1f);
        SetDisable();
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
        gameObject.SetActive(false);
    }
}
