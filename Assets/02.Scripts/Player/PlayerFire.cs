using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총알 프리팹")] 
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    [Header("발사위치")]
    public Transform FirePosition;
    public float FireOffset = 0.3f;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("쿨타임")] 
    public const float CoolTime = 0.6f;
    private float _coolTimer;

    [Header("자동사격")] 
    public bool AutoMode = false;

    
    private void Update()
    {
        // 자동사격 입력
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
            AutoMode = true;
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
            AutoMode = false;
        
        //타이머가 0보다 작으면 발사 가능
        _coolTimer -= Time.deltaTime;
        if (_coolTimer > 0) 
            return; 
        if (Input.GetKey(KeyCode.Space) || AutoMode)
        {
            _coolTimer = CoolTime;
            MakeBullets();
            MakeSubBullets();
        }
    }

    private void MakeBullets()
    {
        GameObject bullet1 = Instantiate(BulletPrefab);
        GameObject bullet2 = Instantiate(BulletPrefab);

        bullet1.transform.position = FirePosition.position + new Vector3(-FireOffset, 0, 0);
        bullet2.transform.position = FirePosition.position + new Vector3(FireOffset, 0, 0);
    }

    private void MakeSubBullets()
    {
        GameObject bullet1 = Instantiate(SubBulletPrefab);
        GameObject bullet2 = Instantiate(SubBulletPrefab);

        bullet1.transform.position = SubFirePositionLeft.position;
        bullet2.transform.position = SubFirePositionRight.position;
    }
}
