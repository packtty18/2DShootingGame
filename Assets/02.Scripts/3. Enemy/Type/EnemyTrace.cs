using UnityEngine;

public class EnemyTrace : EnemyBase
{
    private const float SPRITE_ROTATION_OFFSET = 90f;

    protected override void OnMove()
    {
        Vector2 direction = ((Vector2)_playerTransform.transform.position - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + SPRITE_ROTATION_OFFSET;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;

        transform.position += (Vector3)(direction * _speed * Time.deltaTime);

    }
}
