using UnityEngine;
public class Item : MonoBehaviour
{
    [Header("Item type")]
    public EItemType Type;
    public float Value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Apply(collision);
        Destroy(gameObject);
    }

    private void Apply(Collider2D collision)
    {
        switch (Type)
        {
            case EItemType.MoveSpeedUp:
                {
                    PlayerMove playerMove = collision.GetComponent<PlayerMove>();
                    playerMove.SpeedUp(Value);
                    break;
                }

            case EItemType.AttackSpeedUp:
                {
                    PlayerFire playerFire = collision.GetComponent<PlayerFire>();
                    playerFire.SpeedUp(Value);
                    break;
                }
            case EItemType.HealthUp:
                {
                    PlayerHealth player = collision.GetComponent<PlayerHealth>();
                    player.Heal(Value);
                    break;
                }
        }

        PlayerEffector playerEffector = collision.GetComponent<PlayerEffector>();
        playerEffector.InstantiateEffect(Type);
    }
}
