using System;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private int _followDelay = 30;
    [SerializeField] private float _attackTime =1;

    private Transform _parent;
    private Queue<Vector3> _parentPos;
    private Vector3 _followPos;
    private float _attackTimer;

    private void Awake()
    {
        _parentPos = new Queue<Vector3>();
        
    }
    private void Start()
    {
        _attackTimer = _attackTime;
    }

    private void Update()
    {
        GetMovePosition();
        Move();
        Attack();
    }

    private void Attack()
    {
        _attackTimer -= Time.deltaTime;

        if(_attackTimer < 0 )
        {
            _attackTimer = _attackTime;
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        if(!FactoryManager.IsManagerExist())
        {
            return;
        }

        BulletFactory factory = FactoryManager.Instance.GetFactory<BulletFactory>();
        BulletBase bullet = factory.MakeBullets(EBulletType.PlayerSub, 
            _fireTransform.position, 
            Quaternion.identity).GetComponent<BulletBase>();

        bullet.SetDamage();
        bullet.SetLeft();
      
    }

    public void SetInit(Transform parent)
    {
        _parent = parent;
        _followPos = parent.position;
    }

    private void GetMovePosition()
    {
        if(!_parentPos.Contains(_parent.position))
        {
            _parentPos.Enqueue(_parent.position);
        }
        
        if(_parentPos.Count > _followDelay)
        {
            _followPos = _parentPos.Dequeue();
        }
    }



    private void Move()
    {
        transform.position = _followPos;
    }
}
