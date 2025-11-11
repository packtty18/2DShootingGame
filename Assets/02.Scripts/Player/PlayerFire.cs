using UnityEngine;
using UnityEngine.Windows;

public class PlayerFire : MonoBehaviour
{
    private PlayerStat _stat;
    private PlayerInput _input;

    [Header("Prefabs")] 
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    public GameObject BombBulletPrefab;
    
    [Header("FirePos")]
    public Transform FirePosition;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    private float _coolTimer;

    private void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _input = GetComponent<PlayerInput>();
        _coolTimer = _stat.CoolTime;
    }

    private void Update()
    {
        // 자동사격 입력
        if(_input.IsInputAutoMode)
            _stat.IsAutoMode = !_stat.IsAutoMode;

        if(_input.IsInputSpecialAttack)
        {
            MakeBomb();
        }
        
        //타이머가 0보다 작으면 발사 가능
        _coolTimer -= Time.deltaTime;
        if (_coolTimer > 0) 
            return; 
        if (_input.IsinputFire || _stat.IsAutoMode)
        {
            _coolTimer = _stat.CoolTime;
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

        bullet1.transform.position = FirePosition.position + new Vector3(-_stat.FireOffset, 0, 0);
        bullet2.transform.position = FirePosition.position + new Vector3(_stat.FireOffset, 0, 0);
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

    private void MakeBomb()
    {
        Instantiate(BombBulletPrefab, transform.position, Quaternion.identity);
    }

    public void SpeedUp(float value)
    {
        _coolTimer -= value;
    }
}
