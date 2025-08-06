using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이펙트를 담당하는 클래스
/// </summary>
public class Effect : MonoBehaviour
{
    ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// 이펙트를 재생하는 함수
    /// </summary>
    public void Play()
    {
        _ps.Play(true);
    }

    private void OnParticleSystemStopped()
    {
        gameObject.DestroyOrReturnToPool();
    }
}
