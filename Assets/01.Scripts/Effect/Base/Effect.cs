using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IPoolableComponent
{
    protected ParticleSystem _particleSystem;

    protected Vector2 defaultScale;

    protected virtual void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        defaultScale = transform.localScale;
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, -5);
    }

    public void SetRotation(Vector3 rot)
    {
        transform.rotation = Quaternion.Euler(rot);
    }

    public void Play()
    {
        if(_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();

        _particleSystem.Play();
    }

    public void Despawned()
    {
        
    }

    public void Spawned()
    {
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        transform.localScale = defaultScale;
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    protected virtual void OnParticleSystemStopped()
    {
        SetDisable();
    }

    internal void SetScalse(float s)
    {
        transform.localScale *= s;
    }
}
