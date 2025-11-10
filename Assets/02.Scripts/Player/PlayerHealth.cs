using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStat _stat;

    private float _health;

    private void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _health = _stat.Health;
    }

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
