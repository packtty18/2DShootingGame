using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.
    
    // 필요 속성
    [Header("총알 프리팹")] // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    [Header("총구")]
    public Transform FirePosition;
    public float FireOffset = 0.3f;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("쿨타임")] 
    public const float CoolTime = 0.6f;
    private float _coolTimer;

    [Header("자동모드")] 
    public bool AutoMode = false;

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) AutoMode = true;
        if(Input.GetKeyDown(KeyCode.Alpha2)) AutoMode = false;
        
        
        _coolTimer -= Time.deltaTime;
        if (_coolTimer > 0) return;     // 조기 리턴
        
        // 1. 발사 버튼을 누르고 있거나 (혹은) or == || 자동 모드라면...
        if (Input.GetKey(KeyCode.Space) || AutoMode)
        {
            // 발사하고 나면 쿨타이머를 초기화
            _coolTimer = CoolTime;

            // 유니티에서 게임 오브젝트를 생성할때는 new가 instaintate 라는 메서드를 이용한다.
            // 클래스 -> 객체(속성+기능) -> 메모리에 로드된 객체를 인스턴스
            //                        ㄴ 인스턴스화

            // 메인 총알 생성
            MakeBullets();

            // 보조 총알 생성
            MakeSubBullets();
        }
    }

    private void MakeBullets()
    {
        // 2. 프리팹으로부터 총알(게임 오브젝트)을 생성한다.
        GameObject bullet1 = Instantiate(BulletPrefab);
        GameObject bullet2 = Instantiate(BulletPrefab);

        // 3. 총알의 위치를 총구 위치로 바꾸기 
        bullet1.transform.position = FirePosition.position + new Vector3(-FireOffset, 0, 0);
        bullet2.transform.position = FirePosition.position + new Vector3(FireOffset, 0, 0);
    }

    private void MakeSubBullets()
    {
        // 2. 프리팹으로부터 총알(게임 오브젝트)을 생성한다.
        GameObject bullet1 = Instantiate(SubBulletPrefab);
        GameObject bullet2 = Instantiate(SubBulletPrefab);

        // 3. 총알의 위치를 총구 위치로 바꾸기 
        bullet1.transform.position = SubFirePositionLeft.position;
        bullet2.transform.position = SubFirePositionRight.position;
    }
}
