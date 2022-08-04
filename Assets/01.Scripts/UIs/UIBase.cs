using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class UIBase : MonoBehaviour
{
    public UIDataType mydataType;
    public abstract void Init();
    public abstract void SetData(string data);
}
