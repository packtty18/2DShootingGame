using UnityEngine;

public class EnemyBoss : EnemyBase
{
    private const float MAX_DAMAGE_OF_HITTED = 100;

    [Header("Boss Movement")]
    [SerializeField] private float _spawnSpeed = 2f;  //등장 속도
    [SerializeField] private float _targetY = 4f; // 등장 후 멈출 Y 위치
    [SerializeField] private float _leftLimit = -2f;
    [SerializeField] private float _rightLimit = 2f;

    [Header("Boss Attack")]
    [SerializeField] private Transform _leftShooter;
    [SerializeField] private Transform _rightShooter;
    [SerializeField] private float _minAttackTime = 2;
    [SerializeField] private float _maxAttackTime = 4;
    [SerializeField] private float _attackTimer;

    private bool _spawnComplete = false;
    private bool _moveRight = true;

    protected override void Init()
    {
        base.Init();
        _spawnComplete = false;
        _attackTimer = Random.Range(_minAttackTime, _maxAttackTime);

    }

    protected override void Update()
    {
        OnMove();
        OnAttack();
    }

    private void OnAttack()
    {
        _attackTimer -= Time.deltaTime;

        if(_attackTimer < 0)
        {
            _attackTimer = Random.Range(_minAttackTime, _maxAttackTime); ;
            Attack();
        }
    }

    private void Attack()
    {
        BulletFactory factory = FactoryManager.Instance.GetFactory<BulletFactory>();

        factory.MakeBullets(EBulletType.EnemyDefault, _leftShooter.position, true);
        factory.MakeBullets(EBulletType.EnemyDefault, _rightShooter.position, false);
    }

    //보스가 움직이는 방법
    //생성 후 아래로 내려오고 특정 위치에서 정지후 양 옆으로 이동
    protected override void OnMove()
    {
        if( !_spawnComplete )
        {
            if (transform.position.y <= _targetY)
            {
                _spawnComplete = true;
                return;
            }

            MoveDown();
        }
        else
        {
            MoveHorizontally();
        }
    }

    public void MoveHorizontally()
    {
        Vector3 pos = transform.position;
        float dir = _moveRight ? 1 : -1;
        pos.x += dir * _speed * Time.deltaTime;

        if (pos.x >= _rightLimit)
        {
            _moveRight = false;
        }
        else if (pos.x <= _leftLimit)
        {
            _moveRight = true;
        }
        transform.position = pos;
    }

    // 아래로 내려오는 이동
    public void MoveDown()
    {
        Vector3 pos = transform.position;
        pos.y -= _spawnSpeed * Time.deltaTime;
        transform.position = pos;

        return;
    }
    public override void OnHit(float damage)
    {
        if (!_spawnComplete)
        {
            return;
        }
        base.OnHit(damage);
    }

    protected override void DamageLogic(float damage)
    {
        _health -= Mathf.Min(damage, MAX_DAMAGE_OF_HITTED);
    }


    protected override void Remove()
    {
        if (!_spawnComplete)
        {
            return;
        }
        base.Remove();
    }


    //보스는 플레이어와 충돌해도 파괴되지 않음
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            return;
        }

        health.Hit(_damage);
    }
}
