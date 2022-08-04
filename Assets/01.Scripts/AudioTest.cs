using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    string path = "Prefabs/Effect/DeadEffect";

    private void Start()
    {
        GameObject obj = GameObjectPoolManager.Instance.GetGameObject(path, transform);

        DeadEffect deadEffect = obj.GetComponent<DeadEffect>();

        deadEffect.SetColor(Color.red, Color.blue);
        deadEffect.Play(Vector2.zero);
    }
}
