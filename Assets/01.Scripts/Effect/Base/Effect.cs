using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IPoolableComponent
{
    protected ParticleSystem _particleSystem;

    protected virtual void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Play(Vector2 pos)
    {
        transform.position = pos;
        _particleSystem.Play();
    }

    public void Despawned()
    {
        
    }

    public void Spawned()
    {
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    private void OnParticleSystemStopped()
    {
        SetDisable();
    }
}
