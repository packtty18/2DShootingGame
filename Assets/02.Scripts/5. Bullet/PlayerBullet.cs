using UnityEngine;

public class PlayerBullet : BulletBase
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent(out EnemyHitBox hitBox))
        {
            ApplyDamage(hitBox);
            OnHitTarget();
        }
    }
}
