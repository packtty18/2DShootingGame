using UnityEngine;

public class EnemyBullet : BulletBase
{

    protected override Vector2 GetNewPosition()
    {
        return (Vector2)transform.position + Vector2.down * _speed * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent(out PlayerHealth health))
        {
            ApplyDamage(health);
            OnHitTarget();
        }
    }
}
