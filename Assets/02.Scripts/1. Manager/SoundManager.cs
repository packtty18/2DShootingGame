using UnityEngine;
using System.Collections.Generic;



public class SoundManager : SimpleSingleton<SoundManager>
{
    /*
     * 게임 내에 생성되는 소리 관련 모든 객체를 동작 및 제어
     */
    private const string _bgmObjectName = "BGM";

    [Header("사운드 프리팹")]
    [SerializeField] private GameObject _soundPrefab;   //사운드 오브젝트
    [SerializeField] private Transform _soundParent;

    [Header("정적 생성된 AudioSource")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _playerFireSource;
    [SerializeField] private AudioSource _playerHittedSource;

    [Header("사용할 클립")]
    [SerializeField] private AudioClip _bgmSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _bulletSound;
    [SerializeField] private AudioClip _healSound;
    [SerializeField] private AudioClip _attackSpeedSound;
    [SerializeField] private AudioClip _moveSpeedSound;
    [SerializeField] private AudioClip _playerHitSound;
    [SerializeField] private AudioClip _bombExplosion;
    [SerializeField] private AudioClip _bombLoop;

    [SerializeField] private AudioClip[] _explosionSound;

    private void Start()
    {
        if (_bgmSource == null)
        {
            CreateBGMObject();
        }

        if( _playerFireSource == null|| _playerHittedSource == null)
        {
            Debug.LogError("Player AudioSource Missing");
        }

        
    }

    private void CreateBGMObject()
    {
        GameObject bgmObject = InstantiateSoundObject(_bgmSound, Vector3.zero, _soundParent, false);
        bgmObject.name = _bgmObjectName;
    }

    private GameObject InstantiateSoundObject(AudioClip clip, Vector3 position, Transform parent = null, bool autoDestory = true )
    {
        GameObject soundObject = Instantiate(_soundPrefab, position, Quaternion.identity);
        soundObject.transform.SetParent(parent);

        SoundObject sound = soundObject.GetComponent<SoundObject>();
        sound.SetSound(clip, autoDestory);

        return soundObject;
    }

    public void PlayerSFX(ESFXType type)
    {
        switch(type)
        {
            case ESFXType.PlayerFire:
                {
                    _playerFireSource.Play();
                    break;
                }
            case ESFXType.PlayerHit:
                {
                    _playerHittedSource.Play();
                    break;
                }
        }
    }

    public void PlayItemSound(EItemType itemType, Transform parent)
    {
        switch (itemType)
        {
            case EItemType.HealthUp:
                {
                   CreateSFX(ESFXType.ItemHeal, transform.position, parent);
                    break;
                }
            case EItemType.AttackSpeedUp:
                {
                    CreateSFX(ESFXType.ItemAttackUp, transform.position, parent);
                    break;
                }
            case EItemType.MoveSpeedUp:
                {
                    CreateSFX(ESFXType.ItemMoveUp, transform.position, parent);
                    break;
                }
        }
    }

    public GameObject CreateSFX(ESFXType sfx, Vector3 position, Transform parent = null)
    {
        AudioClip clipToPlay = null;
        bool autoDestroy = true;

        switch (sfx)
        {
            case ESFXType.GameOver:
                {
                    clipToPlay = _gameOverSound;
                    break;
                }
            case ESFXType.ItemHeal:
                {
                    clipToPlay = _healSound;
                    break;
                }
            case ESFXType.ItemAttackUp:
                {
                    clipToPlay = _attackSpeedSound;
                    break;
                }
            case ESFXType.ItemMoveUp:
                {
                    clipToPlay = _moveSpeedSound;
                    break;
                }
                
            case ESFXType.Bomb:
                {
                    clipToPlay = _bombExplosion;
                    break;
                }
            case ESFXType.BombLoop:
                {
                    clipToPlay = _bombLoop;
                    autoDestroy = false;
                }
                break;
            case ESFXType.Explosion:
                {
                    clipToPlay = _explosionSound[Random.Range(0, _explosionSound.Length)];
                    break;
                }
            default:
                {
                    Debug.LogWarning($"Unhandled SFX type: {sfx}");
                    return null;
                }
        }

        GameObject soundGameObject = InstantiateSoundObject(clipToPlay, position, parent, autoDestroy);
        SoundObject soundObject = soundGameObject.GetComponent<SoundObject>();
        soundObject.OnPlay();

        return soundGameObject;
    }
}
