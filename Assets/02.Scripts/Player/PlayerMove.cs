using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerStat _stat;

    private float _speed;

    //시작위치
    private Vector2 _originPosition;

    //자동이동 관련
    private EPlayerMoveState _state;
    private GameObject _targetEnemy = null;


    


    private void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _originPosition = transform.position;
        _speed = _stat.Speed;
        //자동
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject == null)
        {
            Debug.LogError("Can't Find Player Object");
        }

    }
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _speed++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _speed--;
        }

        _speed = Mathf.Clamp(_speed, _stat.MinSpeed, _stat.MaxSpeed);

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
        float finalSpeed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = finalSpeed * _stat.ShiftSpeed;
        }

        if (Input.GetKey(KeyCode.R))
        {
            MoveToOrigin(finalSpeed);
            return;
        }

        //기본 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(h, v);
        direction.Normalize();
        Move(direction, Input.GetKey(KeyCode.LeftShift));
       
    }

    private Vector2 EditValidPosition(Vector2 newPosition)
    {
        if (newPosition.x < _stat.MinX)
        {
            newPosition.x = _stat.MaxX;
        }
        else if (newPosition.x > _stat.MaxX)
        {
            newPosition.x = _stat.MinX;
        }

        newPosition.y = Mathf.Clamp(newPosition.y, _stat.MinY, _stat.MaxY);
        return newPosition;
    }

    
    /// <summary>
    /// Translate 사용하여 원점으로 이동시킬 것
    /// </summary>
    private void MoveToOrigin(float speed)
    {
        //목표방향 = 목표위치 - 현재위치
        Vector2 direction = _originPosition - (Vector2)transform.position;
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SpeedUp(float value)
    {
        _speed += value;
        _speed = Mathf.Min(_stat.MaxSpeed, _speed);
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
            MoveToOrigin(_speed);
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
        if (yDistance >= _stat.YDashMoveInChase)
        {
            Move(Vector2.up, true);
        }
        else if (yDistance >= _stat.YJustMoveInChase)
        {
            Move(Vector2.up);
        }
        else
        {
            _state = EPlayerMoveState.Retreat;
        }

        // X축 이동
        if (Mathf.Abs(xDistance) > _stat.XDistanceToEnemyThreshHoldInChase)
        {
            if (Mathf.Abs(xDistance) > _stat.XDistanceToMoveInChase)
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

        if (yDistance <= _stat.AvoidDistance)
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

        if (yDistance <= _stat.RetreatDistance)
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

            if (yDistance < _stat.YDistanceOnFindTarget || xDistance <= _stat.XDistanceOnFindTarget) 
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
        float moveSpeed = onDash ? _speed * _stat.ShiftSpeed : _speed;
        Vector2 newPos = (Vector2)transform.position + direction.normalized * moveSpeed * Time.deltaTime;
        transform.position = EditValidPosition(newPos);
    }

}
