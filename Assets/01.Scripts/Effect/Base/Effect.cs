using System;
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

    public void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, -5);
    }

    public void SetRotation(Vector3 rot)
    {
        transform.localRotation = Quaternion.Euler(rot);
    }

    public virtual void Play()
    {
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

    protected virtual void OnParticleSystemStopped()
    {
        SetDisable();
    }
}
