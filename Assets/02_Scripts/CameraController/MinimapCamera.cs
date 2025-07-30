using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _miniMapY;

    private void Update()
    {
        Vector3 pos = _target.position;
        pos.y = _miniMapY;

        transform.position = pos;
    }
}
