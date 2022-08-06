using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform titleImage;

    [SerializeField]
    private RectTransform Button;

    [SerializeField]
    private Image bg;

    [SerializeField]
    private GameObject option;

    public void OnEnable()
    {
        bg.DOFade(.5f, 1f);
        titleImage.DOAnchorPos3DY(-414f, 1.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            Button.DOAnchorPos3DY(125, 1f);
        });
    }

    public void StartMain()
    {
        GameManager.Instance.ResetOnSceneChanged();
        SceneManager.LoadScene("Ingame");
    }

    public void OpenOption()
    {
        option.SetActive(true);
    }

    public void StartCradit()
    {

    }
}
