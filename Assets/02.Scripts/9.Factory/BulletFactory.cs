using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    //총알에 대한 생성을 담당
    //즉 BulletBase를 상속받는 클래스들 담당 생성

    [Header("프리팹")]
    [SerializeField] private GameObject _playerDefaultPrefab;
    [SerializeField] private GameObject _playerSubPrefab;
    [SerializeField] private GameObject _playerCurvePrefab;
    [SerializeField] private GameObject _playerBezierPrefab;
    [SerializeField] private GameObject _playerSpiralPrefab;
    [SerializeField] private GameObject _playerBombPrefab;
    [SerializeField] private GameObject _enemyDefaultPrefab;

    private Dictionary<EBulletType, GameObject> _prefabMap;
    [Header("풀링")]
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private Dictionary<EBulletType, GameObject[]>_bulletPool;

    private void Start()
    {
        InitializePrefabMap();
        PoolInit();
    }

    private void InitializePrefabMap()
    {
        _prefabMap = new Dictionary<EBulletType, GameObject>
        {
            { EBulletType.PlayerDefualt, _playerDefaultPrefab },
            { EBulletType.PlayerSub, _playerSubPrefab },
            { EBulletType.PlayerCurve, _playerCurvePrefab },
            { EBulletType.PlayerBezier, _playerBezierPrefab },
            { EBulletType.PlayerSpiral, _playerSpiralPrefab },
            { EBulletType.PlayerBomb, _playerBombPrefab },
            { EBulletType.EnemyDefault, _playerBombPrefab } //임시
        };
    }

    private void PoolInit()
    {
        _bulletPool = new Dictionary<EBulletType, GameObject[]>();
        for(int i =0; i< System.Enum.GetValues(typeof(EBulletType)).Length; i++) 
        {
            EBulletType type = (EBulletType)i;
            _bulletPool[type] = new GameObject[_poolSize];

            for (int j = 0; j < _poolSize; j++)
            {
                GameObject bullet = InstantBullet(type);
                _bulletPool[(EBulletType)i][j] = bullet;
                bullet.transform.SetParent(transform);
            }
        }
    }

    private GameObject InstantBullet(EBulletType type)
    {
        GameObject target = _prefabMap[type];
        GameObject bullet = Instantiate(target);
        bullet.SetActive(false);
        return bullet;
    }

    public void MakeBullets(EBulletType type, Vector3 position, bool isleft = true)
    {
        GameObject target = _prefabMap[type];

        //여기서 풀에서 빼오기
        BulletBase bullet = GetPoolBullet(type).GetComponent<BulletBase>();
       
        bullet.IsLeft = isleft;
        bullet.transform.position = position;

        bullet.InitBullet();
        bullet.gameObject.SetActive(true);
    }

    private GameObject GetPoolBullet(EBulletType type)
    {
        GameObject[] pool = _bulletPool[type];
        for (int i = 0; i < pool.Length; i++)
        {
            GameObject target = pool[i];
            if (target.activeInHierarchy == false)
            {
                return target;
            }
        }

        //풀에 비활성 객체가 없을 경우
        ResizePool(type);
        return GetPoolBullet(type);
    }

    private void ResizePool(EBulletType type)
    {
        GameObject[] pool = _bulletPool[type];
        GameObject[] newPool = new GameObject[pool.Length * 2];

        for (int i = 0; i < pool.Length; i++)
        {
            newPool[i] = pool[i];
        }
        for (int i = pool.Length; i < newPool.Length; i++)
        {
            GameObject bullet = InstantBullet(type);
            newPool[i] = bullet;
            bullet.transform.SetParent(transform);
        }

        _bulletPool[type] = newPool;
    }

}
