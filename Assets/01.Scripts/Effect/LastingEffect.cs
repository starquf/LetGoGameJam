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

        var renderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.sortingLayerName = "Default";
        renderer.sortingOrder = -1;
    }

    public override void Play()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();

        var main = _particleSystem.main;
        main.simulationSpeed = 1f;

        var renderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.sortingLayerName = "Effect";

        Invoke("StopMove", stopMoveTime);

        _particleSystem.Play();
    }
}
