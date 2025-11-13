using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStat _stat;
    private PlayerEffector _effect;

    private float _health;

    private void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _effect = GetComponent<PlayerEffector>();
        _health = _stat.Health;
    }

    public void Hit(float damage)
    {
        
        _health -= damage;
        if (_health <= 0)
        {
            Debug.Log("Player Dead");
            _effect.InstantiateGameOverSoundObject();
            Destroy(gameObject);
        }
        else
        {
            _effect.PlayHitSound();
        }
    }

    internal void Heal(float value)
    {
        _health += value;
    }
}
