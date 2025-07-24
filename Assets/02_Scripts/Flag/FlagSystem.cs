using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSystem : MonoBehaviour
{
    [SerializeField] List<string> _flags;

    /// <summary>
    /// Flag가 현재 있는지 여부를 반환해 주는 함수
    /// </summary>
    /// <param name="flag">검사할 플래그</param>
    /// <returns></returns>
    public bool ContainsFlag(string flag)
    {
        return _flags.Contains(flag);
    }
}
