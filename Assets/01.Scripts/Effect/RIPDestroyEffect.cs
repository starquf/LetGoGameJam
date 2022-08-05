using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RIPDestroyEffect : Effect
{
    private const string RIPWERCKEFFECT_PATH = "Prefabs/Effect/RIPWreckEffect";

    protected override void OnParticleSystemStopped()
    {
        Effect effect = GameObjectPoolManager.Instance.GetGameObject(RIPWERCKEFFECT_PATH, null).GetComponent<Effect>();
        effect.SetPosition(transform.position);
        effect.Play();

        base.OnParticleSystemStopped();
    }
}
