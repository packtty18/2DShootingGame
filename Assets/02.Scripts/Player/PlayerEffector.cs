using UnityEngine;

public class PlayerEffector : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject MoveSpeedUpEffectPrefab;
    public GameObject AttackSpeedUpEffectPrefab;
    public GameObject HealEffectPrefab;

    public void InstantiateMoveSpeedUpEffect()
    {
        Instantiate(MoveSpeedUpEffectPrefab, transform);
    }
    public void InstantiateAttackSpeedUpEffect()
    {
        Instantiate(AttackSpeedUpEffectPrefab, transform);
    }
    public void InstantiateHealEffect()
    {
        Instantiate(HealEffectPrefab, transform);
    }
}
