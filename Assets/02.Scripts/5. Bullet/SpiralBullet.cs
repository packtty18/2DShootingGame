using UnityEngine;

/// <summary>
/// 나선형으로 회전하며 이동하는 총알
/// </summary>
public class SpiralBullet : PlayerBullet
{
    [Header("Debug Sprial")]
    [Tooltip("각도 변화량 정도")]
    [SerializeField] private float _angularSpeed = 10f;

    private Vector2 _center; // 궤적의 중심
    private float _angle;    // 현재 각도
    private float _timer;

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void InitBullet()
    {
        base.InitBullet();
        _center = transform.position;
        _angle = 0f;
        _timer = 0f;
    }

    protected override Vector2 GetNewPosition()
    {
        float dt = Time.deltaTime;
        _timer += dt;

        //중심점. 이점을 중심으로 회전
        _center += (Vector2)transform.up * _speed * dt;
        _angle += _angularSpeed * dt; //시간별 각도의 변화 적용
        
        //코사인과 사인을 활용해 나선궤적 형성
        float offsetX = Mathf.Cos(_angle) ;
        float offsetY = Mathf.Sin(_angle) ;
        Vector2 offset = new Vector2(offsetX, offsetY);

        return _center + offset;
    }
}
