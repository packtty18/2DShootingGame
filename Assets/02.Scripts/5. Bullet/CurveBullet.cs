using UnityEngine;

/// <summary>
/// 옆으로 휘는 총알
/// </summary>
public class CurveBullet : BulletBase
{
    [Header("Debug Curve")]
    [Tooltip("휘는 크기")]
    [SerializeField] private float _amplitude = 3f;  
    [Tooltip("휘는 주기")]
    [SerializeField] private float _frequency = 1f;  

    private float _time;

    protected override void Start()
    {
        base.Start();
        _time = 0f;
    }

    protected override Vector2 GetNewPosition()
    {
        _time += Time.deltaTime;

        float newY = transform.position.y + _speed * Time.deltaTime;
        float directionMultiplier = IsLeft ? -1f : 1f;

        // 좌우로 S자 이동 (sin 함수 사용)
        float newX = transform.position.x + Mathf.Sin(_time * _frequency) * _amplitude * Time.deltaTime * directionMultiplier;
        
        return new Vector2(newX, newY);
    }
}
