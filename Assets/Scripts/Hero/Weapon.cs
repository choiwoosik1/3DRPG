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

    // Mace에 Collider 없어서 안된 걸 수도 체크해보기
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
