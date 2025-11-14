using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : FactoryBase<EEnemyType>
{

    [Header("프리팹")]
    [SerializeField] private GameObject _directEnemyPrefab;
    [SerializeField] private GameObject _chaseEnemyPrefab;
    [SerializeField] private GameObject _teleportEnemyPrefab;

    protected override void RegisterPrefabs()
    {
        _prefabMap = new Dictionary<EEnemyType, GameObject>
        {
            { EEnemyType.Direction, _directEnemyPrefab },
            { EEnemyType.Trace, _chaseEnemyPrefab },
            { EEnemyType.Teleport, _teleportEnemyPrefab }
        };
    }

    public GameObject MakeEnemy(EEnemyType type, Vector3 position)
    {
        EnemyBase enemy = CreateObject(type).GetComponent<EnemyBase>();

        enemy.transform.position = position;
        enemy.gameObject.SetActive(true);
        enemy.OnActiveInit();
        return enemy.gameObject;
    }
}
