using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    /// <summary>
    /// 지정된 대상에게 데미지를 입히는 함수
    /// </summary>
    /// <param name="">데미지를 입히는 대상</param>
    void Hit(IDamageable damageable);

}
