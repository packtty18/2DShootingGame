using UnityEngine;
using System.Collections.Generic;

public class PlayerEffector : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] private GameObject _moveSpeedUpEffectPrefab;
    [SerializeField] private GameObject _attackSpeedUpEffectPrefab;
    [SerializeField] private GameObject _healEffectPrefab;

    private Dictionary<EItemType, GameObject> _effectPrefabs;

    [Header("Sound")]
    private SoundManager _soundManager => SoundManager.Instance;

    private void Awake()
    {
        _effectPrefabs = new Dictionary<EItemType, GameObject>
        {
            { EItemType.MoveSpeedUp, _moveSpeedUpEffectPrefab },
            { EItemType.AttackSpeedUp, _attackSpeedUpEffectPrefab },
            { EItemType.HealthUp, _healEffectPrefab }
        };
    }

    public void InstantiateEffect(EItemType itemType)
    {
        if (_effectPrefabs.TryGetValue(itemType, out GameObject prefab) && prefab != null)
        {
            Instantiate(prefab, transform);
        }
    }

    

    public void InstantiateGameOverSoundObject()
    {
        if (!SoundManager.IsManagerExist())
        {
            Debug.LogError("There's No SoundManager");
            return;
        }
        _soundManager.CreateSFX(ESFXType.GameOver, transform.position);
    }

    public void PlayFireSound()
    {
        if (!SoundManager.IsManagerExist())
        {
            Debug.LogError("There's No SoundManager");
            return;
        }
        _soundManager.PlayerSFX(ESFXType.PlayerFire);
    }

    public void PlayHitSound()
    {
        if (!SoundManager.IsManagerExist())
        {
            Debug.LogError("There's No SoundManager");
            return;
        }
        _soundManager.PlayerSFX(ESFXType.PlayerHit);
    }
}