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
    [SerializeField] private GameObject _gameOverSoundPrefab;
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private AudioSource _fireSound;

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
        Instantiate(_gameOverSoundPrefab);
    }

    public void PlayFireSound()
    {
        _fireSound.Play();
    }

    public void PlayHitSound()
    {
        _hitSound.Play();
    }
}