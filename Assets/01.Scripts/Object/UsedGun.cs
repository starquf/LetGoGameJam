using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UsedGun : MonoBehaviour, IPoolableComponent
{
    private SpriteRenderer sr;

    private Color usedColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        usedColor = sr.color;
    }

    public void Despawned()
    {
        
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    public void Spawned()
    {

    }

    public void SetSprite(Sprite sprite)
    {
        sr.sprite = sprite;
        sr.color = Color.white;
    }

    public void ShowEffect(Vector2 startPos)
    {
        sr.DOColor(usedColor, 1f).SetEase(Ease.Linear);

        StartCoroutine(ParabolaEffect(startPos));
    }

    private IEnumerator ParabolaEffect(Vector2 startPos)
    {
        Vector2 pos = Vector2.right * Random.Range(-3.5f, 3.5f) + Vector2.up * Random.Range(-1.5f, 1.5f);
        float angle = Random.Range(-10f, 10f);

        float t = 0;
        float height = Random.Range(3.0f, 3.5f);

        float speed = Random.Range(1f, 1.1f);

        while (true)
        {
            t += Time.deltaTime * speed;

            transform.position = Parabola(startPos, startPos + pos, height, t);
            transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.z + angle, Vector3.forward);

            if (t > 1)
                yield break;

            yield return null;
        }
    }

    protected Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        float t2 = (-4 * height * t * t) + (4 * height * t);

        Vector3 mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, t2 + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
