using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _health = 3;

    public void Hit(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Debug.Log("Player Dead");
            Destroy(gameObject);
        }
    }

    internal void Heal(float value)
    {
        _health += value;
    }
}
