using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionHandler : Handler
{
    [SerializeField]
    private Dropdown resolutionDropDown;

    [SerializeField]
    private Dropdown screenDropDown;

    [SerializeField]
    private Toggle isShow;

    [SerializeField]
    private Slider masterSoundSlider;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;

    private float time;

    public override void OnAwake()
    {

    }

    public override void OnStart()
    {
        Init();
    }



   
    private void Init()
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        resolutionDropDown.ClearOptions();
        screenDropDown.ClearOptions();

        options.Clear();
        options.Add(new Dropdown.OptionData("1280 * 720 (60)"));
        options.Add(new Dropdown.OptionData("1280 * 720 (144)"));
        options.Add(new Dropdown.OptionData("1600 * 900 (60)"));
        options.Add(new Dropdown.OptionData("1600 * 900 (144)"));
        options.Add(new Dropdown.OptionData("1920 * 1080 (60)"));
        options.Add(new Dropdown.OptionData("1920 * 1080 (144)"));
        options.Add(new Dropdown.OptionData("2560 * 1440 (60)"));
        options.Add(new Dropdown.OptionData("2560 * 1440 (144)"));
        resolutionDropDown.AddOptions(options);

        resolutionDropDown.onValueChanged.AddListener((i) =>
        {
            int x, y, fps;
            string[] datas;

            datas = resolutionDropDown.options[i].text.Split('*','(',')');
            x = int.Parse(datas[0].Trim());
            y = int.Parse(datas[1].Trim());
            fps = int.Parse(datas[2].Trim());

            Screen.SetResolution(x,y,Screen.fullScreenMode);
            Application.targetFrameRate = fps;
        });


        options.Clear();
        options.Add(new Dropdown.OptionData("FullScreen"));
        options.Add(new Dropdown.OptionData("Windowed"));
        screenDropDown.AddOptions(options);
       

        screenDropDown.onValueChanged.AddListener((i) =>
        {
            string data = screenDropDown.options[i].text;
            bool isfull = false;

            if(data.Contains("FullScreen"))
            {
                isfull = true;
            }
            else
            {
                isfull = false;
            }

            Screen.SetResolution(Screen.width, Screen.height, isfull);
        });


        isShow.isOn = false;
        resolutionDropDown.value = 4;
        screenDropDown.value = 0;
        Screen.SetResolution(1920, 1080, true);
        Application.targetFrameRate = 60;
    }


    void OnGUI()
    {
        if (isShow.isOn)
        {
            time += (Time.unscaledDeltaTime - time) * .1f;
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.LowerLeft;
            style.fontSize = 30;
            style.normal.textColor = Color.green;

            float ms = time * 1000f;
            float fps = 1.0f / time;
            string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text, style);
        }
    }
}
