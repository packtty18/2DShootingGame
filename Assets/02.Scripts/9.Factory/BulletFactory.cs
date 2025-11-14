using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : FactoryBase<EBulletType>
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


    protected override void RegisterPrefabs()
    {
        _prefabMap = new Dictionary<EBulletType, GameObject>
        {
            { EBulletType.PlayerDefualt, _playerDefaultPrefab },
            { EBulletType.PlayerSub, _playerSubPrefab },
            { EBulletType.PlayerCurve, _playerCurvePrefab },
            { EBulletType.PlayerBezier, _playerBezierPrefab },
            { EBulletType.PlayerSpiral, _playerSpiralPrefab },
            { EBulletType.PlayerBomb, _playerBombPrefab },
            { EBulletType.EnemyDefault, _enemyDefaultPrefab }
        };
    }

    //전처리까지 해서 반환
    public GameObject MakeBullets(EBulletType type, Vector3 position, Quaternion rotation, bool isleft = true)
    {
        //여기서 풀에서 빼오기
        BulletBase bullet = CreateObject(type).GetComponent<BulletBase>();
       
        bullet.IsLeft = isleft;
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.OnActiveInit();
        bullet.gameObject.SetActive(true);
        return bullet.gameObject;
    }

}
