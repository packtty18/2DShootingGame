using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 직선으로 이동하는 기본 총알
/// </summary>
public class BasicBullet : PlayerBullet
{
    protected override Vector2 GetNewPosition()
    {
        return (Vector2)transform.position + (Vector2)transform.up * _speed * Time.deltaTime;
    }

}
