using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastingEffect : Effect
{
    [SerializeField]
    private float stopMoveTime = 0.2f;

    private void StopMove()
    {
        var main = _particleSystem.main;
        main.simulationSpeed = 0f;
    }

    public override void Play()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();

        Invoke("StopMove", stopMoveTime);

        _particleSystem.Play();
    }
}
