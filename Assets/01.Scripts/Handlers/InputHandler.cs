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
    private ResultHandler resultHandler;
    private PlayerInput playerInput;
    private PlayableDirector director;

    public override void OnAwake()
    {
    }

    public override void OnStart()
    {
        if(isTitle)
        {
            director = Camera.main.GetComponent<PlayableDirector>();
            optionHandler = GameManager.Instance.optionHandler;
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (optionHandler.background.activeInHierarchy)
                {
                    optionHandler.background.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    optionHandler.background.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (optionHandler.background.activeInHierarchy)
                {
                    optionHandler.background.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    optionHandler.background.SetActive(true);
                    Time.timeScale = 0f;
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
                if (!resultHandler.gameObject.activeInHierarchy)
                {
                    resultHandler.SetUI();
                    resultHandler.gameObject.SetActive(true);

                    GameManager.Instance.soundHandler.Play("GameOverBGM");
                }
            }
        }
    }
}
