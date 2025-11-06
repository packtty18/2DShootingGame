using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    public float Duration = 1.2f;

    protected float _speed;
   
    public float Damage;

    public bool IsLeft;

    protected virtual void Start()
    {
        _speed = StartSpeed;
    }

    private void Update()
    {
        float acceleration = (EndSpeed - StartSpeed) / Duration;
        _speed += Time.deltaTime * acceleration;
        
        //속도가 endspeed를 넘어가는 것을 방지
        _speed  = Mathf.Min(_speed, EndSpeed);

        transform.position = GetNewPosition();
    }

    protected virtual Vector2 GetNewPosition()
    {
        //기본 새 위치 = 현재 위치 + 위쪽 방향 * 속도 * 시간
        return (Vector2)transform.position + Vector2.up * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") == false)
            return;

        EnemyHitBox enemyHitBox = collision.GetComponent<EnemyHitBox>();

        if (enemyHitBox == null)
            return;

        Enemy enemy = enemyHitBox.Owner;
        enemy.Hit(Damage * enemyHitBox.DamageMultiplier);
        Destroy(gameObject);
    }
}
