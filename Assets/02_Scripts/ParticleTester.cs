using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleTester : MonoBehaviour
{
    ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        // _ps.main.duration = 2;는 오류 발생 밑과 같이 사용
        ParticleSystem.MainModule main = _ps.main;
        main.duration = 2;

        ParticleSystem.ShapeModule shape = _ps.shape;
        shape.radius = 0.1f;
    }

    private void OnParticleSystemStopped()
    {
        gameObject.DestroyOrReturnToPool();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Particle System이 particle들을 생성하도록 시작
            _ps.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Particle System이 particle들을 더 생성하지 않게 정지
            _ps.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 일시정지
            _ps.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // 생성된 Particle들을 전부 제거
            _ps.Clear(true);
        }
    }
}
