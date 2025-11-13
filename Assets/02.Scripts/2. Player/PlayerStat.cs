using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //start시 초기화할 플레이어의 디버그 스텟
    [Header("Health")]
    public float Health = 3;

    [Header("Move")]
    public float Speed = 3;
    public float MaxSpeed = 5;
    public float MinSpeed = 1;
    public float ShiftSpeed = 1.5f;

    public float MinX = -2;
    public float MaxX = 2;
    public float MinY = -5;
    public float MaxY = 0;

    [Header("AutoMove")]
    public bool IsAutoMode = false;
    public float YDistanceOnFindTarget = 3;
    public float XDistanceOnFindTarget = 1.5f;
    public float RetreatDistance = 4;
    public float AvoidDistance = 3;
    public float YDashMoveInChase = 6f;
    public float YJustMoveInChase = 4;
    public float XDistanceToEnemyThreshHoldInChase = 0.1f;
    public float XDistanceToMoveInChase = 1f;

    [Header("Fire")]
    public float FireOffset = 0.3f;
    public float CoolTime = 0.6f;
}
