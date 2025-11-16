using System;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Health")]
    private float _maxHealth = 3;
    [SerializeField] private float _currentHealth = 3;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    [Header("Move")]
    public float Speed = 3;
    private float _maxSpeed = 5;
    private float _minSpeed = 1;

    [Header("AutoMove")]
    public bool IsAutoMode = false;

    [Header("Fire")]
    public float FireCoolTime = 0.6f;
    public int DamageLevel = 1;

    // 이벤트 선언
    public event Action<float> OnHealthChanged;
    public event Action<float> OnSpeedChanged;
    public event Action<float> OnFireCooltimeChanged;
    public event Action<int> OnDamageLevelChanged;

    // 초기값 세팅 (SaveManager가 호출)
    public void Initialize(float health, float speed, float fireCooltime, int damageLevel)
    {
        _currentHealth = health;
        Speed = speed;
        FireCoolTime = fireCooltime;
        DamageLevel = damageLevel;
    }

    #region Health

    

    public void HealthUp(float value)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + value);
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void HealthDown(float value)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - value);
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public bool IsAbleToDecreaseHealth(float value)
    {
        return _currentHealth - value >= 0;
    }   

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }
    #endregion

    #region Move
    public void SpeedUp(float value)
    {
        Speed = Mathf.Min(_maxSpeed, Speed + value);
        OnSpeedChanged?.Invoke(Speed);
    }

    public void SpeedDown(float value)
    {
        Speed = Mathf.Max(_minSpeed, Speed - value);
        OnSpeedChanged?.Invoke(Speed);
    }
    #endregion

    #region Fire
    public void DecreaseFireCooltime(float value)
    {
        FireCoolTime -= value;
        OnFireCooltimeChanged?.Invoke(FireCoolTime);
    }

    public void IncreaseDamageLevel()
    {
        DamageLevel += 1;
        OnDamageLevelChanged?.Invoke(DamageLevel);
    }
    #endregion
}
