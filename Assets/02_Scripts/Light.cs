using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.UIElements;

public class Light : MonoBehaviour
{
    //[SerializeField] Light _directionLight;
    //[SerializeField] float _rotate;

    [SerializeField] float _timeMultiplier;
    [SerializeField] float _initialTime;

    float _normalizedTime;
    Vector3 _euler;

    private void Start()
    {
        _normalizedTime = _initialTime;
        _euler = transform.eulerAngles;
    }

    void Update()
    {
        //_directionLight.transform.Rotate(Vector3.right * _rotate * Time.deltaTime);

        _normalizedTime += (Time.deltaTime * _timeMultiplier) / 3600 / 24;
        _normalizedTime %= 1;

        float angle = _normalizedTime * 360.0f;
        _euler.x = angle - 90.0f;
        transform.rotation = Quaternion.Euler(_euler);


    }
}
