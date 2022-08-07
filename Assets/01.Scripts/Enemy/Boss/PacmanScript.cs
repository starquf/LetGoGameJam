using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PacmanScript : MonoBehaviour, IPoolableComponent
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer sr;

    public float moveSpeed = 5f;

    private Coroutine pacmanCor;
    private Tween startTween;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();

        coll.enabled = false;
    }

    public void SetPacMan(Vector3 startDir, float startOffset = 0f)
    {
        startTween.Kill();
        startTween = sr.DOFade(0.2f, 1f).SetEase(Ease.Linear).From(0f).OnComplete(() =>
        {
            if (pacmanCor != null)
                StopCoroutine(pacmanCor);

            pacmanCor = StartCoroutine(Logic(startDir, startOffset));
        });
    }

    private IEnumerator Logic(Vector3 startDir, float startOffset)
    {
        Vector3 startPos = Vector3.zero;
        float offset = 2f;

        int currentSpawnPos = 0;
        Vector2 moveDir = Vector2.zero;

        List<float> spawnPosX = new List<float>();
        List<float> spawnPosY = new List<float>();

        Vector3 maxPos = GameManager.Instance.mapMax.position;
        Vector3 minPos = GameManager.Instance.mapMin.position;

        float pos = minPos.x;

        float posDisX = (maxPos.x - minPos.x) / (9f + startOffset);

        for (int i = 0; i < 8; i++)
        {
            pos += posDisX;

            spawnPosX.Add(pos);
        }

        pos = minPos.y;

        float posDisY = (maxPos.y - minPos.y) / (10f + startOffset);

        for (int i = 0; i < 9; i++)
        {
            pos += posDisY;

            spawnPosY.Add(pos);
        }

        if (startDir.Equals(Vector3.left))          // 오른쪽 위
        {
            currentSpawnPos = spawnPosY.Count - 1;

            startPos.x = maxPos.x;
            startPos.y = spawnPosY[currentSpawnPos];

            transform.DOMove(startPos, 1.5f);
            yield return new WaitForSeconds(1.5f);

            sr.DOFade(1f, 1f).SetEase(Ease.Linear);

            coll.enabled = true;

            moveDir = startDir;

            rb.velocity = moveDir * moveSpeed;
            sr.flipX = !sr.flipX;

            while (currentSpawnPos > 0)
            {
                yield return null;

                // 끝까지 갔다면
                if (rb.position.x > maxPos.x + offset || rb.position.x < minPos.x - offset)
                {
                    currentSpawnPos--;

                    rb.position = new Vector2(Mathf.Clamp(rb.position.x, minPos.x, maxPos.x), spawnPosY[currentSpawnPos]);

                    moveDir *= -1f;
                    sr.flipX = !sr.flipX;

                    rb.velocity = moveDir * moveSpeed;
                }
            }
        }
        else if (startDir.Equals(Vector3.right))    // 왼쪽 아래
        {
            currentSpawnPos = 0;

            startPos.x = minPos.x;
            startPos.y = spawnPosY[currentSpawnPos];

            transform.DOMove(startPos, 1.5f);
            yield return new WaitForSeconds(1.5f);

            sr.DOFade(1f, 1f).SetEase(Ease.Linear);

            coll.enabled = true;

            moveDir = startDir;

            rb.velocity = moveDir * moveSpeed;

            while (currentSpawnPos < spawnPosY.Count - 1)
            {
                yield return null;

                // 끝까지 갔다면
                if (rb.position.x > maxPos.x + offset || rb.position.x < minPos.x - offset)
                {
                    currentSpawnPos++;

                    rb.position = new Vector2(Mathf.Clamp(rb.position.x, minPos.x, maxPos.x), spawnPosY[currentSpawnPos]);

                    moveDir *= -1f;
                    sr.flipX = !sr.flipX;

                    rb.velocity = moveDir * moveSpeed;
                }
            }
        }
        else if (startDir.Equals(Vector3.up))       // 왼쪽 아래
        {
            currentSpawnPos = 0;

            startPos.x = spawnPosX[currentSpawnPos];
            startPos.y = minPos.y;

            transform.DOMove(startPos, 1.5f);
            yield return new WaitForSeconds(1.5f);

            sr.DOFade(1f, 1f).SetEase(Ease.Linear);

            coll.enabled = true;

            moveDir = startDir;

            rb.velocity = moveDir * moveSpeed;

            while (currentSpawnPos < spawnPosX.Count - 1)
            {
                yield return null;

                // 끝까지 갔다면
                if (rb.position.y > maxPos.y + offset || rb.position.y < minPos.y - offset)
                {
                    currentSpawnPos++;

                    rb.position = new Vector2(spawnPosX[currentSpawnPos], Mathf.Clamp(rb.position.y, minPos.y, maxPos.y));

                    moveDir *= -1f;
                    sr.flipX = !sr.flipX;

                    rb.velocity = moveDir * moveSpeed;
                }
            }
        }
        else if (startDir.Equals(Vector3.down))     // 오른쪽 위
        {
            currentSpawnPos = spawnPosX.Count - 1;

            startPos.x = spawnPosX[currentSpawnPos];
            startPos.y = maxPos.y;

            transform.DOMove(startPos, 1.5f);
            yield return new WaitForSeconds(1.5f);

            sr.DOFade(1f, 1f).SetEase(Ease.Linear);

            coll.enabled = true;

            moveDir = startDir;

            rb.velocity = moveDir * moveSpeed;
            sr.flipX = !sr.flipX;

            while (currentSpawnPos > 0)
            {
                yield return null;

                // 끝까지 갔다면
                if (rb.position.y > maxPos.y + offset || rb.position.y < minPos.y - offset)
                {
                    currentSpawnPos--;

                    rb.position = new Vector2(spawnPosX[currentSpawnPos], Mathf.Clamp(rb.position.y, minPos.y, maxPos.y));

                    moveDir *= -1f;
                    sr.flipX = !sr.flipX;

                    rb.velocity = moveDir * moveSpeed;
                }
            }
        }

        StopPacMan();
    }

    public void StopPacMan()
    {
        sr.DOFade(0f, 0.5f).SetEase(Ease.Linear).From(1f).OnComplete(() =>
        {
            if (pacmanCor != null)
                StopCoroutine(pacmanCor);

            SetDisable();
        });
    }

    public void Despawned()
    {

    }

    public void Spawned()
    {
        rb.velocity = Vector2.zero;
        sr.flipX = false;
        coll.enabled = false;
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LivingEntity livingEntity = collision.GetComponent<LivingEntity>();
            Hit(livingEntity);
        }
    }

    private void Hit(LivingEntity hitEntity)
    {
        hitEntity.GetDamage(1);
    }
}
