using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("Fire Debug")]
    [Header("Prefabs")] 
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    [Header("FirePos")]
    public Transform FirePosition;
    public float FireOffset = 0.3f;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("CoolTime(s)")] 
    public float CoolTime = 0.6f;
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
        BasicBullet bullet1 = Instantiate(BulletPrefab).GetComponent<BasicBullet>();
        BasicBullet bullet2 = Instantiate(BulletPrefab).GetComponent<BasicBullet>(); ;
        
        bullet1.IsLeft = true;
        bullet2.IsLeft = false;

        bullet1.transform.position = FirePosition.position + new Vector3(-FireOffset, 0, 0);
        bullet2.transform.position = FirePosition.position + new Vector3(FireOffset, 0, 0);
    }

    private void MakeSubBullets()
    {
        BulletBase bullet1 = Instantiate(SubBulletPrefab).GetComponent<BulletBase>();
        BulletBase bullet2 = Instantiate(SubBulletPrefab).GetComponent<BulletBase>();

        bullet1.IsLeft = true;
        bullet2.IsLeft = false;

        bullet1.transform.position = SubFirePositionLeft.position;
        bullet2.transform.position = SubFirePositionRight.position;
    }

    public void SpeedUp(float value)
    {
        CoolTime -= value;
    }
}
