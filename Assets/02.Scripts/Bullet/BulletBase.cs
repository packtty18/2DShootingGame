using System;
using System.Buffers;
using UnityEngine;


/// <summary>
/// 총알의 기본 클래스
/// </summary>
public abstract class BulletBase : MonoBehaviour
{
    [Header("Debug Stat")]
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    public float Duration = 1.2f;
    public float Damage =1;

    protected float _speed;
    protected float _damage;

    [Header("OnLeft")]
    [Tooltip("발사자의 중앙 기준 왼쪽에서 발사된 총알인지 여부")]
    public bool IsLeft;

    protected virtual void Start()
    {
        _speed = StartSpeed;
        _damage = Damage;
    }

    private void Update()
    {
        float acceleration = (EndSpeed - StartSpeed) / Duration;
        _speed += Time.deltaTime * acceleration;

        //속도가 endspeed를 넘어가는 것을 방지
        _speed = Mathf.Min(_speed, EndSpeed);

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
