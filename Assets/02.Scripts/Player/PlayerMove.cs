using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [Header("속도")]
    public float Speed = 3;
    public float MaxSpeed = 5;
    public float MinSpeed = 1;
    public float ShiftSpeed = 1.5f;

    private float _speed;

    [Header("시작위치")]
    private Vector2 _originPosition;
    
    [Header("이동범위")]
    public float MinX = -2;
    public float MaxX =  2;
    public float MinY = -5;
    public float MaxY =  0;

    public bool IsAutoMode = false;

    private void Start()
    {
        _originPosition = transform.position;
        _speed = Speed;
    }
    
    
    private void Update()
    {
        if (IsAutoMode)
        {

            return;
        }
        //Q와 E로 속도 조절
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed--;
        }

        //속도 제한.
        //speed가 mSpeed보다 작으면 minSpeed로 설정.
        //maxSpeed보다 크면 maxSpeed로 설정.
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
        
        float finalSpeed = Speed;

        //Shift키를 누르면 가속
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = finalSpeed * ShiftSpeed;
        }

        //R키를 누르면 원점으로 이동
        if (Input.GetKey(KeyCode.R))
        {
            MoveToOrigin(finalSpeed);
            return;
        }
        
        //기본 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); 
        
        Vector2 direction = new Vector2(h, v);
        direction.Normalize();
        Vector2 position = transform.position; 
        
        //다음 프레임 위치 계산
        Vector2 newPosition = position + direction * finalSpeed * Time.deltaTime;

        //이동 범위 제한 + 좌우 끝으로 이동시 반대편으로 이동
        //newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }
        
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        //최종 위치 적용
        transform.position = newPosition;        
    }

    /// <summary>
    /// Translate 사용하여 원점으로 이동시킬 것
    /// </summary>
    /// <param name="speed"></param>
    private void MoveToOrigin(float speed)
    {
        //목표방향 = 목표위치 - 현재위치
        Vector2 direction = _originPosition - (Vector2)transform.position;
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SpeedUp(float value)
    {
        _speed += value;
        _speed = Mathf.Min(MaxSpeed, _speed);
    }
}
