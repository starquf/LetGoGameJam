using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Handler
{

    private OptionHandler optionHandler;
    private InGameUIHandler uIHandler;
    private GameObject minimap;

    public override void OnAwake()
    {
    }

    public override void OnStart()
    {
        optionHandler = GameManager.Instance.optionHandler;
        uIHandler = GameManager.Instance.inGameUIHandler;
        minimap = GameObject.Find("MiniMap");
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(optionHandler.gameObject.activeInHierarchy)
            {
                optionHandler.gameObject.SetActive(false);
            }
            else
            {
                optionHandler.gameObject.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            if(minimap.activeInHierarchy)
            {
                minimap.SetActive(false);
            }
            else
            {
                minimap.SetActive(true);
            }
        }
    }
}
