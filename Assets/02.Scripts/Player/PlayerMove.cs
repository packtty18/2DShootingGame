using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [Header("속도")]
    public float Speed = 3;
    public float MaxSpeed = 5;
    public float MinSpeed = 1;
    public float ShiftSpeed = 1.5f;

    private bool _onDash = false;

    [Header("시작위치")]
    private Vector2 _originPosition;
    
    [Header("이동범위")]
    public float MinX = -2;
    public float MaxX =  2;
    public float MinY = -5;
    public float MaxY =  0;


    [Header("자동")]
    public bool IsAutoMode = false;

    [Header("Debug")]
    [SerializeField]
    private EPlayerMoveState _state;
    [SerializeField]
    private GameObject _targetEnemy = null;


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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed--;
        }

        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        if (IsAutoMode)
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
        float finalSpeed = Speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = finalSpeed * ShiftSpeed;
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
        Vector2 position = transform.position;

        //다음 프레임 위치 계산
        Vector2 newPosition = position + direction * finalSpeed * Time.deltaTime;

        //이동 범위 제한 + 좌우 끝으로 이동시 반대편으로 이동
        //newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        newPosition = EditValidPosition(newPosition);

        //최종 위치 적용
        transform.position = newPosition;
    }

    private Vector2 EditValidPosition(Vector2 newPosition)
    {
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);
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
        Speed += value;
        Speed = Mathf.Min(MaxSpeed, Speed);
    }

    #region AutoMode
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
            MoveToOrigin(Speed);
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
        if (yDistance >= 6) 
            Move(Vector2.up, true);
        else if (yDistance >= 4)
            Move(Vector2.up);
        else 
            _state = EPlayerMoveState.Retreat;

        // X축 이동
        if (Mathf.Abs(xDistance) > 0.1f)
        {
            if (Mathf.Abs(xDistance) > 1f)
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

        if (yDistance <= 3)
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

        if (yDistance <= 4)
        {
            Move(Vector2.down);
        }
    }

    private bool FindTarget()
    {
        SortedDictionary<int, GameObject> enemies = EnemyObserver.Instance.GetDictionary();
        GameObject nearestEnemy = null;
        float minDistance = 100; //임시

        foreach (GameObject enemyObject in enemies.Values)
        {
            if (enemyObject == null) 
                continue;

            float yDistance = enemyObject.transform.position.y - transform.position.y;
            float xDistance = Mathf.Abs(enemyObject.transform.position.x - transform.position.x);

            if (yDistance < 3 || xDistance <= 1.5f) 
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
    #endregion

    #region Movement
    private void Move(Vector2 direction, bool onDash = false)
    {
        float moveSpeed = onDash ? Speed * ShiftSpeed : Speed;
        Vector2 newPos = (Vector2)transform.position + direction.normalized * moveSpeed * Time.deltaTime;
        transform.position = EditValidPosition(newPos);
    }

    #endregion
}
