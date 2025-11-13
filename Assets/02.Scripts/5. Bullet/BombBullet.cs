using UnityEngine;

public class BombBullet : BulletBase
{
    //현재 위치에서 0,0,0으로 이동
    [Header("Bomb")]
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private GameObject _effect;

    [SerializeField] private Vector2 _targetPos = Vector2.zero;
    [SerializeField] private float _threshold = 0.1f;
    private Vector2 _direction;


    protected override void Start()
    {
        base.Start();
    }

    public override void InitBullet()
    {
        base.InitBullet();
        //목표지점의 방향 도출
        _direction = (_targetPos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        //목표지점을 바라보도록 회전
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;
    }

    protected override void Update()
    {
        if(Vector2.Distance(_targetPos, transform.position) < _threshold)
        {
            Explosion();
        }
        base.Update();
    }

    protected override Vector2 GetNewPosition()
    {
        return (Vector2)transform.position + _direction * _speed * Time.deltaTime;
    }

    private void Explosion()
    {
        CreateEffect();
        CreateBombExplosion();
        OnHitTarget();
    }

    private void CreateBombExplosion()
    {
        GameObject explosion = Instantiate(_bombPrefab, _targetPos, Quaternion.identity);
        if (SoundManager.IsManagerExist())
        {
            SoundManager.Instance.CreateSFX(ESFXType.BombLoop, explosion.transform.position, explosion.transform);
        }
    }

    private void CreateEffect()
    {
        GameObject effect = Instantiate(_effect, _targetPos, Quaternion.identity);

        if(SoundManager.IsManagerExist())
        {
            SoundManager.Instance.CreateSFX(ESFXType.Bomb, effect.transform.position, effect.transform);
        }
    }
}
