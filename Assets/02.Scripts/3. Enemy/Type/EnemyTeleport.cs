using UnityEngine;

public class EnemyTeleport : EnemyBase
{
    protected override void OnMove()
    {
       base.OnMove();

    }

    public override void OnHit(float damage)
    {
        if (_health > 0)
        {
            float randomX = Random.Range(-2f, 2f);
            transform.position = new Vector2(randomX, transform.position.y);
        }

        base.OnHit(damage);
    }
}
