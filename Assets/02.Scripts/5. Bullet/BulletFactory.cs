using System;
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

    public void MakeBullets(EBulletType type, Vector3 position, bool isleft = true)
    {
        GameObject target = GetPrefabBytype(type);
        BulletBase bullet = Instantiate(target).GetComponent<BulletBase>();

        bullet.IsLeft = isleft;
        bullet.transform.position = position;
        bullet.transform.SetParent(transform);
    }

    private GameObject GetPrefabBytype(EBulletType type)
    {
        GameObject target = null;
        switch(type) 
        {
            case EBulletType.PlayerDefualt:
                {
                    target = _playerDefaultPrefab;
                    break;
                }
            case EBulletType.PlayerCurve:
                {
                    target = _playerCurvePrefab;
                    break;
                }
            case EBulletType.PlayerSub:
                {
                    target = _playerSubPrefab;
                    break;
                }
            case EBulletType.PlayerBezier:
                {
                    target = _playerBezierPrefab;
                    break;
                }
            case EBulletType.PlayerSpiral:
                {
                    target = _playerSpiralPrefab;
                    break;
                }
            case EBulletType.PlayerBomb:
                {
                    target = _playerBombPrefab;
                    break;
                }
            case EBulletType.EnemyDefault:
                {
                    //아직 없음
                    break;
                }

        }

        return target;
    }
}
