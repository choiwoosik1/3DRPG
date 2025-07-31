using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] Light _directionLight;
    [SerializeField] float _rotate;

    void Update()
    {
        _directionLight.transform.Rotate(Vector3.right * _rotate * Time.deltaTime);
    }
}
