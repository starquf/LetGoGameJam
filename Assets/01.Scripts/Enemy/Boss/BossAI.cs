using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private List<BossState> states = new List<BossState>();

    public BossState startState;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InitState();
    }

    private void InitState()
    {
        GetComponentsInChildren(states);

        for (int i = 0; i < states.Count; i++)
        {
            states[i].Init(rb, anim);
        }
    }

    private void Start()
    {
        StartState();
    }

    private void StartState()
    {
        PlayState(startState);
    }

    private void MoveToNextState()
    {
        BossState nextState = GetRandomState();

        PlayState(nextState);
    }

    private void PlayState(BossState state)
    {
        state.Play(MoveToNextState);
    }

    private BossState GetRandomState()
    {
        return states[Random.Range(0, states.Count)];
    }
}
