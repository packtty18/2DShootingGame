using CartoonFX;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerFire : MonoBehaviour
{
    private PlayerStat _stat;
    private PlayerInput _input;
    private PlayerEffector _effect;

    [Header("Prefabs")] 
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _subBulletPrefab;
    [SerializeField] private GameObject _bombBulletPrefab;
    
    [Header("FirePos")]
    [SerializeField] private Transform _firePosition;
    [SerializeField] private Transform _subFirePositionLeft;
    [SerializeField] private Transform _subFirePositionRight;

    private float _coolTimer;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _input = GetComponent<PlayerInput>();
        _effect = GetComponent<PlayerEffector>();
    }

    private void Start()
    {
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

            _effect.PlayFireSound();
        }
    }

    private void MakeBullets()
    {
        BasicBullet bullet1 = Instantiate(_bulletPrefab).GetComponent<BasicBullet>();
        BasicBullet bullet2 = Instantiate(_bulletPrefab).GetComponent<BasicBullet>(); ;
        
        bullet1.IsLeft = true;
        bullet2.IsLeft = false;

        bullet1.transform.position = _firePosition.position + new Vector3(-_stat.FireOffset, 0, 0);
        bullet2.transform.position = _firePosition.position + new Vector3(_stat.FireOffset, 0, 0);
    }

    private void MakeSubBullets()
    {
        BulletBase bullet1 = Instantiate(_subBulletPrefab).GetComponent<BulletBase>();
        BulletBase bullet2 = Instantiate(_subBulletPrefab).GetComponent<BulletBase>();

        bullet1.IsLeft = true;
        bullet2.IsLeft = false;

        bullet1.transform.position = _subFirePositionLeft.position;
        bullet2.transform.position = _subFirePositionRight.position;
    }

    private void MakeBomb()
    {
        Instantiate(_bombBulletPrefab, transform.position, Quaternion.identity);
    }

    public void SpeedUp(float value)
    {
        _coolTimer -= value;
    }
}
