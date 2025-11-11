using UnityEngine;

public class PlayerEffector : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _moveSpeedUpEffectPrefab;
    [SerializeField] private GameObject _attackSpeedUpEffectPrefab;
    [SerializeField] private GameObject _healEffectPrefab;

    private System.Collections.Generic.Dictionary<EItemType, GameObject> _effectPrefabs;

    private void Awake()
    {
        _effectPrefabs = new System.Collections.Generic.Dictionary<EItemType, GameObject>
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
}