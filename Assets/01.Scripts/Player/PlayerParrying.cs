using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParrying : MonoBehaviour
{
    private PlayerInput playerInput;
    private Collider2D parryingCol;
    private bool isCoolTime;

    public void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        parryingCol = GetComponent<CircleCollider2D>();
        parryingCol.enabled = false;
        StartCoroutine(Parrying());
    }

    private IEnumerator Parrying()
    {
        while(true)
        {
            if(playerInput.isParrying)
            {
                if(isCoolTime)
                {
                    yield return new WaitForSeconds(5f);
                    isCoolTime = false;
                }
                else
                {
                    GameManager.Instance.soundHandler.Play("MeleeAttack");
                    isCoolTime = true;
                    parryingCol.enabled = true;
                    yield return new WaitForSeconds(0.05f);
                    parryingCol.enabled = false;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
