using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] CombatCharacterModel _model;
    [SerializeField] float _damage;
    [SerializeField] protected LayerMask _targetLayerMask;

    private void Start()
    {
        _damage = _model.Damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if(enemy != null)
            {
                //_model.Hit(enemy)
            }
        }
    }

}
