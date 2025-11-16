using CartoonFX;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.Rendering.DebugUI;

public class PlayerFire : MonoBehaviour
{
    private const int DAMAGE_INCREASE_SCORE = 300;
    private const float DAMAGE_INCREASE_RATE = 0.1f;
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
    private float _coolTime;
    private float _coolTimer;
    private float _damageMultipliers;


    [SerializeField] private bool _isReadyToFire;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _input = GetComponent<PlayerInput>();
        _effect = GetComponent<PlayerEffector>();
    }

    private void Start()
    {
        _coolTime = _stat.CoolTime;
        _coolTimer = _coolTime;
        _damageMultipliers = 1;
        _isReadyToFire = false;
    }

    private void Update()
    {
        // 자동사격 입력
        if (_input.IsInputAutoMode)
            _stat.IsAutoMode = !_stat.IsAutoMode;

        if (_input.IsInputSpecialAttack)
        {
            OnBomb();
        }

        _coolTimer -= Time.deltaTime;
        if (_coolTimer > 0)
        {
            return;
        }
        else if( !_isReadyToFire)
        {
            _isReadyToFire = true;
        }

        if (_input.IsinputFire || _stat.IsAutoMode)
        {
            OnFire();
        }
    }

    public void OnBomb()
    {
        MakeBomb();
    }


    public void OnFire()
    {
        if(!_isReadyToFire)
        {
            return;
        }

        _isReadyToFire = false;
        _coolTimer = _coolTime;
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
        BulletBase bullet1 = factory.MakeBullets(_mainBulletType, 
            _firePosition.position + new Vector3(-_stat.FireOffset, 0, 0),
            Quaternion.identity).GetComponent<BulletBase>();
        bullet1.SetLeft(true);
        bullet1.SetDamage(_damageMultipliers);

        BulletBase bullet2 = factory.MakeBullets(_mainBulletType,
            _firePosition.position + new Vector3(_stat.FireOffset, 0, 0),
            Quaternion.identity).GetComponent<BulletBase>();
        bullet2.SetLeft(false);
        bullet2.SetDamage(_damageMultipliers);
    }

    private void MakeSubBullets()
    {
        if (!FactoryManager.IsManagerExist())
        {
            return;
        }

        BulletFactory factory = FactoryManager.Instance.GetFactory<BulletFactory>();
        BulletBase bullet1 = factory.MakeBullets(_subBulletType,
            _subFirePositionLeft.position,
            Quaternion.identity).GetComponent<BulletBase>();
        bullet1.SetLeft(true);
        bullet1.SetDamage(_damageMultipliers);

        BulletBase bullet2 = factory.MakeBullets(_subBulletType,
            _subFirePositionLeft.position,
            Quaternion.identity).GetComponent<BulletBase>();
        bullet2.SetLeft(false);
        bullet2.SetDamage(_damageMultipliers);
    }

    private void MakeBomb()
    {
        if (!FactoryManager.IsManagerExist())
        {
            return;
        }

        BulletFactory factory = FactoryManager.Instance.GetFactory<BulletFactory>();
        factory.MakeBullets(EBulletType.PlayerBomb, _firePosition.position, Quaternion.identity);
    }

    public void SpeedUp(float value)
    {
        _coolTime -= value;
    }

    

    public void DamageUp()
    {
        _damageMultipliers += DAMAGE_INCREASE_RATE;
        if(!ScoreManager.IsManagerExist())
        {
            return;
        }

        ScoreManager score = ScoreManager.Instance;
        if (score.IsScoreCanReduce(DAMAGE_INCREASE_SCORE))
        {
            score.ReduceScore(DAMAGE_INCREASE_SCORE);
        }
    }
}
