using UnityEngine;

public class SBullet : Bullet
{
    //좌우 흔들림 = 사인곡선 궤적
    // x= 진폭 * sin(주기 * 시간)
    public float Amplitude = 3f;   // 좌우 진폭
    public float Frequency = 1f;     // S자 주기 속도

    private float _time;

    protected override void Start()
    {
        base.Start();
        _time = 0f;
    }

    protected override Vector2 GetNewPosition()
    {
        _time += Time.deltaTime;

        // 위쪽으로 이동 (기본 속도 적용)
        float newY = transform.position.y + _speed * Time.deltaTime;

        // 좌우로 S자 이동 (sin 함수 사용)
        float newX = transform.position.x + Mathf.Sin(_time * Frequency) * Amplitude * Time.deltaTime;

        return new Vector2(newX, newY);
    }
}
