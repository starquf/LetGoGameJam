using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Handler
{
    public bool isTitle = false;

    private OptionHandler optionHandler;
    private InGameUIHandler uIHandler;
    private PopUpInfoHandler popUpInfoHandler;
    private GameObject minimap;
    private ResultHandler resultHandler;
    private PlayerInput playerInput;

    public override void OnAwake()
    {
    }

    public override void OnStart()
    {
        if(isTitle)
        {

        }
        else
        {
            resultHandler = GameManager.Instance.resultHandler;
            optionHandler = GameManager.Instance.optionHandler;
            uIHandler = GameManager.Instance.inGameUIHandler;
            popUpInfoHandler = GameManager.Instance.popUpInfoHandler;
            playerInput = GameManager.Instance.playerTrm.GetComponent<PlayerInput>();
            minimap = GameObject.Find("MiniMap");
            resultHandler.gameObject.SetActive(false);
            optionHandler.gameObject.SetActive(false);
            minimap.SetActive(false);
            popUpInfoHandler.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if(isTitle)
        {

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                /*if(popUpInfoHandler.gameObject.activeInHierarchy)
                {
                    popUpInfoHandler.gameObject.SetActive(false);
                }
                else */
                if (optionHandler.gameObject.activeInHierarchy)
                {
                    optionHandler.gameObject.SetActive(false);
                }
                else
                {
                    optionHandler.gameObject.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (minimap.activeInHierarchy)
                {
                    minimap.SetActive(false);
                }
                else
                {
                    minimap.SetActive(true);
                }
            }

            if (Input.GetKey(KeyCode.Tab))
            {
                popUpInfoHandler.gameObject.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                popUpInfoHandler.gameObject.SetActive(false);
            }

            if(GameManager.Instance.playerTrm.GetComponent<PlayerInput>().isDie)
            {
                resultHandler.SetUI();
                resultHandler.gameObject.SetActive(true);
            }
        }
    }
}
