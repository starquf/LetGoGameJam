using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceInfoScripts : MonoBehaviour , IPoolableComponent
{
    public Image sprite;
    public Text text;

    public void Despawned()
    {
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    public void Spawned()
    {
    }


    public void SetData(Sprite _sprite, string level)
    {
        sprite.sprite = _sprite;
        text.text = "LV." + level;
    }

    public void LevelUp(string level)
    {
        text.text = "LV." + level;
    }

}
