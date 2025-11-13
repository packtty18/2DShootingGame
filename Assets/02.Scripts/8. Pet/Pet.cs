using System;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private GameObject _bulletPrefab;
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
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _fireTransform.position;
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
        //else if( _parentPos.Count < _followDelay)
        //{
        //    _followPos = _parent.position;
        //}
    }



    private void Move()
    {
        transform.position = _followPos;
    }
}
