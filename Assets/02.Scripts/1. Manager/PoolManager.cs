using System.Collections.Generic;
using UnityEngine;

public enum EPoolType
{
    Bullet,
    Item,
    Enemy
}


public class PoolManager : SimpleSingleton<PoolManager>
{
    //오브젝트의 풀링을 관리
    //풀링 대상 : 총알, 아이템, 적
    //풀 딕셔너리 관리 => 키(종류)와 값(객체의 배열)
    //초기화 -> 딕셔너리의 객체 종류별로 3~5개씩 미리 생성
    //생성 => 딕셔너리에 비활성화된 객체가 있는지 확인 -> 비활성 객체를 활성화하고 배열에서 제거후 반환 -> 없다면 생성
    //파괴 => 해당 객체를 비활성화하고 배열에 삽입
    //[SerializeField] private int poolCount;
    //private Dictionary<EBulletType, Queue<GameObject>> _bulletPool;
    //private Dictionary<EEnemyType, Queue<GameObject>> _enemyPool;

    //BulletFactory _bulletFactory;

    //private void Start()
    //{
    //    _bulletFactory = GameObject.Find("BulletFactory").GetComponent<BulletFactory>(); 
    //}

    //public void PoolInit()
    //{
    //    _bulletPool = new Dictionary<EBulletType, Queue<GameObject>>();
    //    _enemyPool = new Dictionary<EEnemyType, Queue<GameObject>>();

    //    for(int i=0; i< System.Enum.GetValues(typeof(EBulletType)).Length; i++)
    //    {
    //        _bulletPool[(EBulletType)i] = new Queue<GameObject>();
    //        for(int j =0; j< poolCount; i++)
    //        {
    //            _bulletFactory.MakeBullets((EBulletType)i, Vector3.zero);
    //        }
    //    }
    //}

    ////풀에서 하나 뽑아 / 없으면 생성해서 활성화하여 반환
    //public GameObject DequeuePool(EPoolType type)
    //{
    //    GameObject target = null;

    //    return target;
    //}

    ////해당 오브젝트를 비활성화 후 풀에 삽입
    //public void EnqueuePool(GameObject poolObject)
    //{

    //}

}
