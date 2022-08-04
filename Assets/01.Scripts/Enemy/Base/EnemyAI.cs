using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Enemy myLivingEntity;
    Animator myAnim;

    public Transform playerTrm;

    State curState;

    private void Start()
    {
        myLivingEntity = this.GetComponent<Enemy>();
        myAnim = this.GetComponent<Animator>();

        curState = new PursueAndAttack(this.gameObject, myLivingEntity, myAnim, playerTrm);
    }

    private void Update()
    {
        curState = this.curState.Process();
    }
}
