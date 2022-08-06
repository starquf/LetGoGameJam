using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Enemy myLivingEntity;
    Animator myAnim;

    private Transform playerTrm;
    private bool isActive = false;

    EnemyState curState;

    public void InitAI(Enemy livingEntity)
    {
        isActive = true;
        myLivingEntity = livingEntity;
        myAnim = this.GetComponent<Animator>();
        playerTrm = myLivingEntity.playerTrm;


        curState = new EnemyPurseState(this.gameObject, myLivingEntity, myAnim, playerTrm);
    }

    private void Update()
    {
        if(isActive)
        {
            curState = this.curState.Process();
        }
    }

    public void SetActive(bool enable)
    {
        isActive = enable;
    }
}
