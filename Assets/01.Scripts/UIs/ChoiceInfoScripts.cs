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


    public void SetData(Sprite _sprite, string level , bool isinfinity)
    {
        sprite.sprite = _sprite;
        if(isinfinity)
        {
            text.text = "X" + level;
        }
        else
        {
            text.text = "LV." + level;
        }
    }

    public void LevelUp(string level, bool isinfinity)
    {
        if (isinfinity)
        {
            text.text = "X" + level;
        }
        else
        {
            text.text = "LV." + level;
        }
    }

}
