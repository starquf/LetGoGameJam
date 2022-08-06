using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class DialogInfo
{
    public string text;
    public Transform teller;
}

public class DialogUI : UIBase
{

    private const string TEXTPREFAB_PATH = "Prefabs/UI/Dialog";

    public Camera main;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetData(JsonUtility.ToJson(new DialogInfo() { text = "씨발 민수", teller = GameManager.Instance.playerTrm}));
        }
    }


    public override void Init()
    {
        mydataType = UIDataType.Dialog;
    }

    public override void SetData(string data)
    {
        print(data);
        DialogInfo dialogInfo = JsonUtility.FromJson<DialogInfo>(data);

        Dialog dialogTextObj = GameObjectPoolManager.Instance.GetGameObject(TEXTPREFAB_PATH, dialogInfo.teller).GetComponentInChildren<Dialog>();
        Canvas cv = dialogTextObj.GetComponent<Canvas>();
        if(cv != null)
        {
            cv.worldCamera = Camera.main;
        }
        else
        {
            print("씨발 지랄 ㄴ");
        }
        Text dialogText = dialogTextObj.dialogText;


        dialogText.text = dialogInfo.text;

        dialogTextObj.transform.position = dialogInfo.teller.GetComponent<LivingEntity>().dialogTrm.position;//.SetPosition(mainCam.WorldToScreenPoint(dialogInfo.position));
    }



}
