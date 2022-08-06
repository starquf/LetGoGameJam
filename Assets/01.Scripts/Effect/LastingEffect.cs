using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastingEffect : Effect
{
    [SerializeField]
    private float stopMoveTime = 0.2f;

    private WaitForSeconds ws;
    private Coroutine co;

    protected override void Awake()
    {
        ws = new WaitForSeconds(stopMoveTime);
        base.Awake();
    }

    public override void Play()
    {
        var main = _particleSystem.main;
        main.simulationSpeed = 1f;

        var renderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.sortingLayerName = "Effect";

        _particleSystem.Play();

        if (co != null)
        {
            StopCoroutine(co);
        }

        co = StartCoroutine(DisableTimer());
    }

    private IEnumerator DisableTimer()
    {
        yield return ws;

        var main = _particleSystem.main;
        main.simulationSpeed = 0f;

        var renderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.sortingLayerName = "Default";
        renderer.sortingOrder = -1;
    }

    protected override void OnParticleSystemStopped()
    {
        Debug.LogWarning("아니 왜안죽냐고 ㅆ발");
        base.OnParticleSystemStopped();
    }
}
