using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyObserver : SimpleSingleton<EnemyObserver>
{
    public int Id;

    //private 변경가능성 존재s
    private SortedDictionary<int, GameObject> _enemyList;

    private void Start()
    {
        Id = 0;
        _enemyList = new SortedDictionary<int, GameObject>();
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
        //Debug.Log($"ID 등록 : {Id}");
        return Id++;
    }

    public void RemoveEnemy(int removeId)
    {
        if (!_enemyList.ContainsKey(removeId))
        {
            return;
        }

        //Debug.Log($"ID 삭제 : {removeId}");
        _enemyList.Remove(removeId);
    }

    [ContextMenu("Debug Enemies")]
    public void DebugEnemies()
    {
        Debug.Log("Search Enemies");
        Debug.Log("count :"  +  _enemyList.Count);
        foreach (var enemy in _enemyList)
        {
            GameObject enemyObject = enemy.Value;
            if (enemyObject == null)
            {
                Debug.LogError("RemoveError : " + enemy.Key);
            }

            Enemy enemyInstance = enemyObject.GetComponent<Enemy>();
            

            Debug.Log("id : " + enemyInstance.GetID() + " name : " + enemy.Value.name);
        }
        Debug.Log("Search End");
    }
}
