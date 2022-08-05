using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    string path = "Prefabs/Effect/DeadEffect";

    private void Start()
    {
        Invoke("Test", 5.0f);
    }

    private void Test()
    {
        GameObject obj = GameObjectPoolManager.Instance.GetGameObject(path, null);

        ColorEffect deadEffect = obj.GetComponent<ColorEffect>();

        deadEffect.SetColor(Color.red, Color.blue);
        deadEffect.Play();
    }
}
