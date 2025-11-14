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
    [SerializeField] private Transform _centerShooter;
    [SerializeField] private Transform _leftShooter;
    [SerializeField] private Transform _rightShooter;
    [SerializeField] private float _minAttackTime = 2;
    [SerializeField] private float _maxAttackTime = 4;
    [SerializeField] private float _attackTimer;

    [Header("Attack Settings")]
    [SerializeField] private int circleBulletCount = 10;
    [SerializeField] private int fanBulletCount = 3;
    [SerializeField] private float fanAngle = 60f;

    private BulletFactory _bulletFactory => FactoryManager.Instance.GetFactory<BulletFactory>();
    private bool _spawnComplete = false;
    private bool _moveRight = true;

    public BossSpawner Spawner;

    protected override void Init()
    {
        base.Init();
        _spawnComplete = false;
        _attackTimer = Random.Range(_minAttackTime, _maxAttackTime);
    }

    public void SetSpawner(BossSpawner spawner)
    {
        Spawner = spawner;
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
            RandomAttack();
        }
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
        Spawner.ResetBossSpawn();
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

    int attackType = 0;
    private void RandomAttack()
    {
        //int attackType = Random.Range(0, 3);
        
        switch (attackType)
        {
            case 0: Attack_SingleShot(); break;
            case 1: Attack_CircleShot(); break;
            case 2: Attack_FanShotToPlayer(); break;
        }
        attackType = (attackType + 1) % 3;
    }

    private void Attack_SingleShot()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, -180f);
        _bulletFactory.MakeBullets(EBulletType.EnemyDefault, _leftShooter.position, rotation,  true);
        _bulletFactory.MakeBullets(EBulletType.EnemyDefault, _rightShooter.position, rotation, false);
    }

    private void Attack_CircleShot()
    {
        float angleStep = 360f / circleBulletCount;
        float angle = 0f;

        for (int i = 0; i < circleBulletCount; i++)
        {
            float radian = angle * Mathf.Deg2Rad;
            Vector3 position = _centerShooter.position;
            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0);

            Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            // 부채꼴/원형은 방향 계산 후 bullet 내부에서 처리
            _bulletFactory.MakeBullets(EBulletType.EnemyDefault,position + direction * 0.5f,rotation , direction.x < 0);

            angle += angleStep;
        }
    }

    private void Attack_FanShotToPlayer()
    {
        if (_playerTransform == null) 
            return;

        Vector3 bossPos = _centerShooter.position;
        Vector3 toPlayer = (_playerTransform.position - bossPos).normalized;
        float baseAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        float startAngle = baseAngle - (fanAngle / 2f);
        float step = fanAngle / (fanBulletCount - 1);

        for (int i = 0; i < fanBulletCount; i++)
        {
            float angle = startAngle + step * i;
            float radian = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            _bulletFactory.MakeBullets(EBulletType.EnemyDefault, bossPos, rotation,  direction.x < 0);
        }
    }
}

