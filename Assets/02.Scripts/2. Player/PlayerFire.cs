using CartoonFX;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerFire : MonoBehaviour
{
    [Header("FirePos")]
    [SerializeField] private Transform _firePosition;
    [SerializeField] private Transform _subFirePositionLeft;
    [SerializeField] private Transform _subFirePositionRight;

    [Header("Bullet Type")]
    [SerializeField] private EBulletType _mainBulletType = EBulletType.PlayerDefualt;
    [SerializeField] private EBulletType _subBulletType = EBulletType.PlayerCurve;
    
    private PlayerStat _stat;
    private PlayerInput _input;
    private PlayerEffector _effect;
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
            Fire();
        }
    }

    public void Fire()
    {
        _coolTimer = _stat.CoolTime;
        MakeBullets();
        MakeSubBullets();
        _effect.PlayFireSound();
    }

    private void MakeBullets()
    {
        if (!FactoryManager.IsManagerExist())
        {
            return;
        }
        BulletFactory factory = FactoryManager.Instance.GetFactory<BulletFactory>();
        factory.MakeBullets(_mainBulletType, _firePosition.position + new Vector3(-_stat.FireOffset, 0, 0),Quaternion.identity, true);
        factory.MakeBullets(_mainBulletType, _firePosition.position + new Vector3(_stat.FireOffset, 0, 0), Quaternion.identity, false);
    }

    private void MakeSubBullets()
    {
        if (!FactoryManager.IsManagerExist())
        {
            return;
        }

        BulletFactory factory = FactoryManager.Instance.GetFactory<BulletFactory>();
        factory.MakeBullets(_subBulletType, _subFirePositionLeft.position, Quaternion.identity, true);
        factory.MakeBullets(_subBulletType, _subFirePositionRight.position, Quaternion.identity, false);
    }

    private void MakeBomb()
    {
        if (!FactoryManager.IsManagerExist())
        {
            return;
        }

        BulletFactory factory = FactoryManager.Instance.GetFactory<BulletFactory>();
        factory.MakeBullets(EBulletType.PlayerBomb, _firePosition.position, Quaternion.identity, true);
    }

    public void SpeedUp(float value)
    {
        _coolTimer -= value;
    }
}
