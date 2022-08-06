using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Handler
{

    private OptionHandler optionHandler;
    private InGameUIHandler uIHandler;
    private PopUpInfoHandler popUpInfoHandler;
    private GameObject minimap;

    public override void OnAwake()
    {
    }

    public override void OnStart()
    {
        optionHandler = GameManager.Instance.optionHandler;
        uIHandler = GameManager.Instance.inGameUIHandler;
        popUpInfoHandler = GameManager.Instance.popUpInfoHandler;
        minimap = GameObject.Find("MiniMap");
        optionHandler.gameObject.SetActive(false);
        minimap.SetActive(false);
        popUpInfoHandler.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            /*if(popUpInfoHandler.gameObject.activeInHierarchy)
            {
                popUpInfoHandler.gameObject.SetActive(false);
            }
            else */if(optionHandler.gameObject.activeInHierarchy)
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

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(popUpInfoHandler.gameObject.activeInHierarchy)
            {
                popUpInfoHandler.gameObject.SetActive(false);
            }
            else
            {
                popUpInfoHandler.gameObject.SetActive(true);
            }
        }
    }
}
