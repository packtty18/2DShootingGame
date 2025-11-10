using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public PlayerStat stat;

    [Header("Prefabs")] 
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    [Header("FirePos")]
    public Transform FirePosition;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    private float _coolTimer;

    private void Start()
    {
        PlayerStat stat = GetComponent<PlayerStat>();
        _coolTimer = stat.CoolTime;
    }

    private void Update()
    {
        // 자동사격 입력
        if(Input.GetKeyDown(KeyCode.Alpha1))
            stat.IsAutoMode = true;
        if(Input.GetKeyDown(KeyCode.Alpha2))
            stat.IsAutoMode = false;
        
        //타이머가 0보다 작으면 발사 가능
        _coolTimer -= Time.deltaTime;
        if (_coolTimer > 0) 
            return; 
        if (Input.GetKey(KeyCode.Space) || stat.IsAutoMode)
        {
            _coolTimer = stat.CoolTime;
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

        bullet1.transform.position = FirePosition.position + new Vector3(-stat.FireOffset, 0, 0);
        bullet2.transform.position = FirePosition.position + new Vector3(stat.FireOffset, 0, 0);
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
        _coolTimer -= value;
    }
}
