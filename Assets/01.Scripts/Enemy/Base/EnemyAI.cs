using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Enemy myLivingEntity;
    Animator myAnim;

    private Transform playerTrm;

    EnemyState curState;

    private void Start()
    {
        myLivingEntity = this.GetComponent<Enemy>();
        myAnim = this.GetComponent<Animator>();
        playerTrm = myLivingEntity.playerTrm;

        curState = new PursueAndAttack(this.gameObject, myLivingEntity, myAnim, playerTrm);
    }

    private void Update()
    {
        curState = this.curState.Process();
    }
}
