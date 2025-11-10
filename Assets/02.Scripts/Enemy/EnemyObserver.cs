using System.Collections.Generic;
using UnityEngine;

public class EnemyObserver : MonoBehaviour
{
    private static EnemyObserver instance;
    public static EnemyObserver Instance
    {
        get
        {
            if(instance == null)
                instance = new EnemyObserver();
            return instance;
        }
    }
    public int Id;

    //private 변경가능성 존재s
    private SortedDictionary<int, GameObject> _enemyList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Id = 0;
            _enemyList = new SortedDictionary<int, GameObject>();
        }
        else
        {
            return;
        }
    }

    public SortedDictionary<int, GameObject> GetDictionary()
    {
        return _enemyList;
    }

    /// <summary>
    /// 적을 등록하고 해당 등록한 ID를 반환
    /// </summary>
    public int InsertEnemy(GameObject enemy)
    {
        _enemyList.Add(Id, enemy);
        Id++;
        return Id;
    }

    public void RemoveEnemy(int id)
    {
        if (!_enemyList.ContainsKey(id))
        {
            return;
        }

        _enemyList.Remove(id);
    }

    [ContextMenu("Debug Enemies")]
    public void DebugEnemies()
    {
        Debug.Log("Search Enemies");
        foreach (var enemy in _enemyList)
        {
            Enemy enemyInstance = enemy.Value.GetComponent<Enemy>();
            Debug.Log("id : " + enemyInstance.GetID() + " name : " + enemy.Value.name);
        }
        Debug.Log("Search End");
    }
}
