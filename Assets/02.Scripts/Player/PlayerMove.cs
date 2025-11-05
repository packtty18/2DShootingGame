using Unity.VisualScripting;
using UnityEngine;

// 플레이어 이동
public class PlayerMove : MonoBehaviour
{
    // 목표
    // "키보드 입력"에 따라 "방향"을 구하고 그 방향으로 이동시키고 싶다.
    
    // 구현 순서:
    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동
    
    // 필요 속성:
    [Header("능력치")]
    public float Speed = 3;
    public float MaxSpeed = 10;
    public float MinSpeed = 1;
    public float ShiftSpeed = 1.2f;
    
    [Header("시작위치")]
    private Vector2 _originPosition;
    
    
    [Header("이동범위")]
    public float MinX = -2;
    public float MaxX =  2;
    public float MinY = -5;
    public float MaxY =  0;

    private void Start()
    {
        // 처음 시작 위치 저장
        _originPosition = transform.position;
    }
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed++;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Speed--;
        }
        
        // 1 ~ 10
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
        
        float finalSpeed = Speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 1.2 ~ 12
            finalSpeed = finalSpeed * ShiftSpeed;
        }

        if (Input.GetKey(KeyCode.R))
        {
            // 원점으로 돌아간다.
            TranslateToOrigin(finalSpeed);
            return;
        }
        
        
        // 1. 키보드 입력을 감지한다. 
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든것을 담당하다.
        float h = Input.GetAxisRaw("Horizontal"); // 수평 입력에 대한 값을 -1, 0, 1로 가져온다.
        float v = Input.GetAxisRaw("Vertical");   // 수직 입력에 대한 값을 -1, 0, 1로 가져온다.
        
        
        // 2. 입력으로부터 방향을 구한다.
        // 벡터: 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);
        
        // 2-1. 방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize();
        // direction = direction.normalized;
        
        Debug.Log($"direction: {direction.x}, {direction.y}");
        
        // 오른쪽        (1, 0)
        // 위쪽          0. 1)
        // 대각선위오른쪽 (1, 1)
        
        
        // 3. 그 방향으로 이동을한다.
        Vector2 position = transform.position; // 현재 위치
        
        
        // 쉬운 버전
        // transform.Translate(direction * Speed * Time.deltaTime);
        
        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        //       새로운 위치 = 현재 위치  +  방향     *  속력   * 시간
        Vector2 newPosition = position + direction * finalSpeed * Time.deltaTime;  // 새로운 위치
        
        
        // Time.deltaTime: 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지.. 나타내는 값
        //                 1초 / fps 값과 비슷하다.
        
        // 이동속도 : 10
        // 컴퓨터1 :  50FPS : Update -> 초당 50번  실행 -> 10 * 50  = 500   * Time.deltaTime = 두개의 값이 같아진다.
        // 컴퓨터2 : 100FPS : Update -> 초당 100번 실행 -> 10 * 100 = 1000  * Time.deltaTime
        
        // -1, 0, 1, 0.00000001 이 숫자 3개 말고는 다 매직넘버이므로 변수로 빼야된다.
        
        // 1. 포지션 값에 제한을 둔다.
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);
        
        
        transform.position = newPosition;         // 새로운 위치로 갱신
    }

    private void TranslateToOrigin(float speed)
    {
        // 방향을 구한다.
        Vector2 direction = _originPosition - (Vector2)transform.position;
        
        // 이동을 한다.
        transform.Translate(direction * speed * Time.deltaTime);
    }
    
    
}
