using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IPoolableComponent
{
    public int healVal = 1;

    private SpriteRenderer sr = null;

    private bool isTimer = false;

    private float defaultDestroyTimer = 30f;
    private float destoryTimer = 0;
    private float fadeVal = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Despawned()
    {

    }

    public void Spawned()
    {
        fadeVal = 100;
        destoryTimer = 100;
        isTimer = false;
        sr.color = Color.white;
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);

        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (GameManager.Instance.timeScale <= 0f)
            return;

        if (isTimer)
        {
            destoryTimer -= Time.deltaTime;
            if (destoryTimer < 0f)
            {
                SetDisable();
            }
            else if (destoryTimer < 5)
            {
                float speed = Mathf.Clamp((50 / destoryTimer), 10, 30);
                fadeVal += Time.deltaTime * speed;
                sr.color = new Color(1, 1, 1, Mathf.Cos(fadeVal));
            }
        }
    }
    public void SetDestoryTimer(float time)
    {
        defaultDestroyTimer = time;
        destoryTimer = defaultDestroyTimer;
        fadeVal = 0;
        isTimer = true;
    }
}
