using System;
using System.Buffers;
using UnityEngine;


/// <summary>
/// 총알의 기본 클래스
/// </summary>
public abstract class BulletBase : MonoBehaviour
{
    [Header("Debug Stat")]
    [SerializeField] private float _startSpeed = 1f;
    [SerializeField] private float _endSpeed = 7f;
    [SerializeField] private float _duration = 1.2f;
    [SerializeField] private float _statDamage =1;

    protected float _speed;
    protected float _damage;

    [Header("OnLeft")]
    [Tooltip("발사자의 중앙 기준 왼쪽에서 발사된 총알인지 여부")]
    public bool IsLeft;

    protected virtual void Start()
    {
        _speed = _startSpeed;
        _damage = _statDamage;
    }

    private void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null)
            return;

        if(collision.TryGetComponent<EnemyHitBox>(out EnemyHitBox hitBox))
        {
            ApplyDamage(hitBox);
            OnHitTarget();
        }
    }

    /// <summary>
    /// 히트박스를 가진 적에게 데미지 적용
    /// </summary>
    private void ApplyDamage(EnemyHitBox hitBox)
    {
        if (hitBox == null)
            return;
        Enemy enemy = hitBox.Owner;
        enemy?.Hit(_damage * hitBox.DamageMultiplier);
    }

    /// <summary>
    /// 총알이 적중했을 때의 효과(파티클, 소리 등)
    /// </summary>
    protected virtual void OnHitTarget()
    {
        Destroy(gameObject);
    }
}
