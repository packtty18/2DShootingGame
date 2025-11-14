using UnityEngine;

/// <summary>
/// 총알의 기본 클래스
/// </summary>
public abstract class BulletBase : MonoBehaviour, IPoolable
{
    [Header("BulletBase")]
    [SerializeField] private float _startSpeed = 1f;
    [SerializeField] private float _endSpeed = 7f;
    [SerializeField] private float _duration = 1.2f;
    [SerializeField] private float _statDamage =1;

    protected float _speed;
    protected float _damage;

    [HideInInspector]
    public bool IsLeft;

    protected virtual void Start()
    {
        InitBullet();
    }

    public void OnActiveInit()
    {
        InitBullet();
    }

    public void OnDeactive()
    {
        gameObject.SetActive(false);
    }

    protected virtual void InitBullet()
    {
        _speed = _startSpeed;
        _damage = _statDamage;
    }

    protected virtual void Update()
    {
        float acceleration = (_endSpeed - _startSpeed) / _duration;
        _speed += Time.deltaTime * acceleration;

        //속도가 endspeed를 넘어가는 것을 방지
        _speed = Mathf.Min(_speed, _endSpeed);

        transform.position = GetNewPosition();
    }

    /// <summary>
    /// 총알 별 움직이는 방법 정의
    /// </summary>
    protected virtual Vector2 GetNewPosition()
    {
        return (Vector2)transform.position + Vector2.up * _speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null)
            return;
    }

    /// <summary>
    /// 히트박스를 가진 적에게 데미지 적용
    /// </summary>
    protected void ApplyDamage(EnemyHitBox hitBox)
    {
        if (hitBox == null)
            return;
        EnemyBase enemy = hitBox.Owner;
        enemy?.OnHit(_damage * hitBox.DamageMultiplier);
    }

    protected void ApplyDamage(PlayerHealth health)
    {
        if (health == null)
            return;

        health.Hit(_damage);
    }

    /// <summary>
    /// 총알이 적중했을 때의 효과
    /// </summary>
    protected virtual void OnHitTarget()
    {
        OnDeactive();
    }
}
