public enum EItemType
{
    MoveSpeedUp,
    HealthUp,
    AttackSpeedUp
}

public enum EEnemyType
{
    Direction,
    Trace,
    Teleport
}

public enum EPlayerAIStateType
{
    Idle,
    Attack,
    Retreat
}

public enum EPlayerMoveState
{
    Idle,
    Chase,
    Retreat
}

public enum ESFXType
{ 
    GameOver,
    PlayerFire,
    Bomb,
    BombLoop,
    Explosion,
    ItemHeal,
    ItemAttackUp,
    ItemMoveUp,
    PlayerHit
}

public enum EBulletType
{
    PlayerDefualt,
    PlayerCurve,
    PlayerSub,
    PlayerBezier,
    PlayerSpiral,
    PlayerBomb,
    EnemyDefault,
}
