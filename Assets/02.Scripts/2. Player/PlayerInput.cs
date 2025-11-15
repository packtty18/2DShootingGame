using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; } = Vector2.zero;

    // 대쉬
    public bool IsInputDash { get; private set; } = false;

    // 기타 액션 입력
    public bool IsinputFire { get; private set; } = false;
    public bool IsInputOrigin { get; private set; } = false;
    public bool IsInputAutoMode { get; private set; } = false;
    
    public bool IsInputSpecialAttack { get; private set; } = false;
    public bool IsInputSpawnPet { get; private set; } = false;
    public bool IsInputDestroyPet { get; private set; } = false;
    public bool IsInputSpeedUp { get; private set; } = false;
    public bool IsInputSpeedDown { get; private set; } = false;
    
    private void Update()
    {
        Vector2 moveInput = Vector2.zero;
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(h, v).normalized;

        //모바일 빌드시에는 풀기
        //if (UIManager.IsManagerExist())
        //{
        //    var ui = UIManager.Instance;
        //    DynamicJoystick joystick = ui.GetJoystick();
        //    moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        //}

        MoveDirection = moveInput;
        // 데스크톱: 키보드 입력
        IsinputFire = Input.GetKey(KeyCode.Space);

        IsInputDash = Input.GetKey(KeyCode.LeftShift);
        IsinputFire = Input.GetKey(KeyCode.Space);
        IsInputOrigin = Input.GetKey(KeyCode.R);
        IsInputAutoMode = Input.GetKeyDown(KeyCode.Alpha1);
        
        IsInputSpecialAttack = Input.GetKeyDown(KeyCode.Alpha3);
        IsInputSpawnPet = Input.GetKeyDown(KeyCode.Alpha4);
        IsInputDestroyPet = Input.GetKeyDown(KeyCode.Alpha5);
        IsInputSpeedUp = Input.GetKeyDown(KeyCode.Q);
        IsInputSpeedDown = Input.GetKeyDown(KeyCode.E);
    }

}

