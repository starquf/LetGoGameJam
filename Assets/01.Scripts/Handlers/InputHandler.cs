using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InputHandler : Handler
{
    public bool isTitle = false;

    private OptionHandler optionHandler;
    private InGameUIHandler uIHandler;
    private PopUpInfoHandler popUpInfoHandler;
    private GameObject minimap;
    private PlayableDirector director;

    public override void OnAwake()
    {
    }

    public override void OnStart()
    {
        if(isTitle)
        {
            director = Camera.main.GetComponent<PlayableDirector>();
        }
        else
        {
            optionHandler = GameManager.Instance.optionHandler;
            uIHandler = GameManager.Instance.inGameUIHandler;
            popUpInfoHandler = GameManager.Instance.popUpInfoHandler;
            minimap = GameObject.Find("MiniMap");
            optionHandler.background.SetActive(false);
            minimap.SetActive(false);
            popUpInfoHandler.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if(isTitle)
        {
            if(Input.GetMouseButtonDown(0) && director.time < 11)
            {
                director.time = 11f;
            }

            if(director.time >= 15)
            {
                director.Stop();
            }
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
                if (optionHandler.background.activeInHierarchy)
                {
                    optionHandler.background.SetActive(false);
                }
                else
                {
                    optionHandler.background.SetActive(true);
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
        }
    }
}
