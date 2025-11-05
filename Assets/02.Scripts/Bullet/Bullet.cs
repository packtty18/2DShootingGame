using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 목표: 위로 계속 이동하고 싶다.

    // 필요 속성
    [Header("이동")] 
    public float StartSpeed   = 1f;
    public float EndSpeed     = 7f;
    public float Duration     = 1.2f;
    private float _speed;

    private void Start()
    {
        _speed = StartSpeed;
    }

    private void Update()
    {
        // 목표: Duration 안에 EndSpeed까지 달성하고 싶다.
        
        // 논리적인 실수(1), 코딩 컨벤션(1)
        float acceleration = (EndSpeed - StartSpeed) / Duration;
        //                     6      / 1.2   = 5
        _speed += Time.deltaTime * acceleration;   // 초당 + 1 * 가속도
        _speed  = Mathf.Min(_speed, EndSpeed);
        //         ㄴ 어떤 속성과 어떤 메서드를 가지고 있는지 톺아볼 필요가 있다.
        
        
        // 방향을 구한다.
        Vector2 direction = Vector2.up;

        // 공식에 따라 이동한다.
        // 새로운 위치는 = 현재 위치 + 방향 * 속력 * 시간
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
