using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    private PlayerStat _stat;
    private PlayerInput _input;
    private Animator _animator;

    private float _minX = -2;
    private float _maxX = 2;
    private float _minY = -5;
    private float _maxY = 0;

    
    private float _shiftSpeedMultiplier = 1.5f;

    //시작위치
    private Vector2 _originPosition;

    //자동이동 관련
    private EPlayerMoveState _state;
    private GameObject _targetEnemy = null;
    public float YDistanceOnFindTarget = 3;
    public float XDistanceOnFindTarget = 1.5f;
    public float RetreatDistance = 4;
    public float AvoidDistance = 3;
    public float YDashMoveInChase = 6f;
    public float YJustMoveInChase = 4;
    public float XDistanceToEnemyThreshHoldInChase = 0.1f;
    public float XDistanceToMoveInChase = 1f;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _originPosition = transform.position;
        //자동
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject == null)
        {
            Debug.LogError("Can't Find Player Object");
        }

    }

    private void Update()
    {
        if (_input.IsInputSpeedUp)
        {
            _stat.SpeedUp(1);
        }
        else if (_input.IsInputSpeedDown)
        {
            _stat.SpeedDown(1);
        }

        if (_stat.IsAutoMode)
        {
            OnAutoMode();
        }
        else
        {
            OnControlMode();
        }
    }

    private void OnControlMode()
    {
        if (_input.IsInputOrigin)
        {
            MoveToOrigin();
            return;
        }

        Move(_input.MoveDirection, _input.IsInputDash);
       
    }

    private Vector2 EditValidPosition(Vector2 newPosition)
    {
        if (newPosition.x < _minX)
        {
            newPosition.x = _maxX;
        }
        else if (newPosition.x > _maxX)
        {
            newPosition.x = _minX;
        }

        newPosition.y = Mathf.Clamp(newPosition.y, _minY, _maxY);
        return newPosition;
    }

    
    /// <summary>
    /// Translate 사용하여 원점으로 이동시킬 것
    /// </summary>
    private void MoveToOrigin()
    {
        //목표방향 = 목표위치 - 현재위치
        Vector2 direction = _originPosition - (Vector2)transform.position;
        float speed = _input.IsInputDash ? _stat.Speed * _shiftSpeedMultiplier : _stat.Speed;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    


    private void OnAutoMode()
    {
        switch (_state)
        {
            case EPlayerMoveState.Idle:
                {
                    IdleUpdate(); 
                    break;
                }
            case EPlayerMoveState.Chase:
                {
                    ChaseUpdate(); 
                    break;
                }
            case EPlayerMoveState.Retreat:
                {
                    RetreatUpdate(); 
                    break;
                }
        }
    }

    private void IdleUpdate()
    {
        if (!FindTarget())
        {
            MoveToOrigin();
        }
        else
        {
            _state = EPlayerMoveState.Chase;
        }
    }

    private void ChaseUpdate()
    {
        if (_targetEnemy == null)
        {
            _state = EPlayerMoveState.Idle;
            return;
        }

        Vector2 direction = (Vector2)_targetEnemy.transform.position - (Vector2)transform.position;
        float yDistance = direction.y;
        float xDistance = direction.x;

        // Y축 이동
        if (yDistance >=YDashMoveInChase)
        {
            Move(Vector2.up, true);
        }
        else if (yDistance >= YJustMoveInChase)
        {
            Move(Vector2.up);
        }
        else
        {
            _state = EPlayerMoveState.Retreat;
        }

        // X축 이동
        if (Mathf.Abs(xDistance) > XDistanceToEnemyThreshHoldInChase)
        {
            if (Mathf.Abs(xDistance) >XDistanceToMoveInChase)
            {
                Move(xDistance < 0 ? Vector2.left : Vector2.right, true);
            }
            else
            {
                Move(xDistance < 0 ? Vector2.left : Vector2.right);
            }
        }
    }
    
    private void RetreatUpdate()
    {
        if (_targetEnemy == null)
        {
            _state = EPlayerMoveState.Idle;
            return;
        }

        Vector2 direction = (Vector2)_targetEnemy.transform.position - (Vector2)transform.position;
        float yDistance = direction.y;

        if (yDistance <= AvoidDistance)
        {
            if (!FindTarget())
            {
                _state = EPlayerMoveState.Idle;
            }
            else
            {
                _state = EPlayerMoveState.Chase;
            }
            return;
        }

        if (yDistance <= RetreatDistance)
        {
            Move(Vector2.down);
        }
    }

    
    private bool FindTarget()
    {
        if (EnemyObserver.Instance == null)
        {
            Debug.Log("There's No EnemyObserver");
            return false;
        }

        SortedDictionary<int, GameObject> enemies = EnemyObserver.Instance.GetDictionary();
        GameObject nearestEnemy = null;
        float minDistance = 100; //임시

        foreach (GameObject enemyObject in enemies.Values)
        {
            if (enemyObject == null) 
                continue;

            float yDistance = enemyObject.transform.position.y - transform.position.y;
            float xDistance = Mathf.Abs(enemyObject.transform.position.x - transform.position.x);

            if (yDistance <YDistanceOnFindTarget || xDistance <= XDistanceOnFindTarget) 
                continue;

            float distance = enemyObject.transform.position.sqrMagnitude;
            if (distance < minDistance)
            {
                nearestEnemy = enemyObject;
                minDistance = distance;
            }
        }

        _targetEnemy = nearestEnemy;
        return _targetEnemy != null;
    }

    private void Move(Vector2 direction, bool onDash = false)
    {
        _animator.SetInteger("x", (int)direction.x);

        float moveSpeed = onDash ? _stat.Speed * _shiftSpeedMultiplier : _stat.Speed;
        Vector2 newPos = (Vector2)transform.position + direction.normalized * moveSpeed * Time.deltaTime;
        transform.position = EditValidPosition(newPos);
    }

}
