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
    public float size;
}

public class DialogUI : UIBase
{

    private const string TEXTPREFAB_PATH = "Prefabs/UI/Dialog";

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
        //print(data);
        DialogInfo dialogInfo = JsonUtility.FromJson<DialogInfo>(data);

        Dialog dialogTextObj = GameObjectPoolManager.Instance.GetGameObject(TEXTPREFAB_PATH, dialogInfo.teller).GetComponentInChildren<Dialog>();
       
        Text dialogText = dialogTextObj.dialogText;


        dialogText.text = dialogInfo.text;
        dialogTextObj.SetPosition(dialogInfo.size);
        dialogTextObj.transform.position = dialogInfo.teller.GetComponentInChildren<LivingEntity>().dialogTrm.position;//.SetPosition(mainCam.WorldToScreenPoint(dialogInfo.position));
    }



}
