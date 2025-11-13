using UnityEngine;

/// <summary>
/// 나선형으로 회전하며 이동하는 총알
/// </summary>
public class SpiralBullet : BulletBase
{
    [Header("Debug Sprial")]
    [Tooltip("각도 변화량 정도")]
    [SerializeField] private float _angularSpeed = 10f;

    private Vector2 center; // 궤적의 중심
    private float angle;    // 현재 각도
    private float time;

    protected override void Start()
    {
        base.Start();
        center = transform.position;
        angle = 0f;
        time = 0f;
    }

    protected override Vector2 GetNewPosition()
    {
        float dt = Time.deltaTime;
        time += dt;

        //중심점. 이점을 중심으로 회전
        center += Vector2.up * _speed * dt;
        angle += _angularSpeed * dt; //시간별 각도의 변화 적용
        
        //코사인과 사인을 활용해 나선궤적 형성
        float offsetX = Mathf.Cos(angle) ;
        float offsetY = Mathf.Sin(angle) ;
        Vector2 offset = new Vector2(offsetX, offsetY);

        return center + offset;
    }
}
