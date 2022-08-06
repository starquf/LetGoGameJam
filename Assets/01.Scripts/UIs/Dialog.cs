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
        if (textPostionRoutine != null)
        {
            StopCoroutine(textPostionRoutine);
        }
    }

    public void Spawned()
    {
        if(textPostionRoutine != null)
        {
            StopCoroutine(textPostionRoutine);
        }
      
    }

    public void SetPosition(Vector3 position)
    {
        //textPostionRoutine = StartCoroutine(PositionRoutine(position));
    }

    private IEnumerator PositionRoutine(Vector3 position)
    {
        while (true)
        {
            transform.position = position;
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void SetDisable()
    {
        throw new System.NotImplementedException();
    }
}
