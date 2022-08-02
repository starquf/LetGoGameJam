using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager m_Instance;
    public static EffectManager Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = FindObjectOfType<EffectManager>();
            return m_Instance;
        }
    }

    public void PlayEffect(PoolObjectType type, Vector3 pos) => StartCoroutine(GenerateEffectPool(type, pos));
    public void PlayEffect(PoolObjectType type, Vector3 pos, Quaternion rot) => StartCoroutine(GenerateEffectPool(type, pos, rot));
    
    private IEnumerator GenerateEffectPool(PoolObjectType type, Vector3 pos)
    {
        GameObject ob = PoolManager.Instance.GetPoolObject(type);

        ob.transform.position = pos;
        ob.transform.rotation = Quaternion.identity;

        ParticleSystem particleSystem = ob.GetComponent<ParticleSystem>();
        float time = particleSystem.main.startLifetime.constant;

        yield return new WaitForSeconds(time);

        PoolManager.Instance.CoolObject(ob, type);
    }

    private IEnumerator GenerateEffectPool(PoolObjectType type, Vector3 pos, Quaternion rot)
    {
        GameObject ob = PoolManager.Instance.GetPoolObject(type);

        ob.transform.position = pos;
        ob.transform.rotation = rot;

        ParticleSystem particleSystem = ob.GetComponent<ParticleSystem>();
        float time = particleSystem.main.startLifetime.constant;

        yield return new WaitForSeconds(time);

        PoolManager.Instance.CoolObject(ob, type);
    }
}