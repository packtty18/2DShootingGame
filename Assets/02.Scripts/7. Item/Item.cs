using UnityEngine;
public class Item : MonoBehaviour
{
    [Header("Item type")]
    [SerializeField] private EItemType _type;
    [SerializeField] private float _value;

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
        switch (_type)
        {
            case EItemType.MoveSpeedUp:
                {
                    PlayerMove playerMove = collision.GetComponent<PlayerMove>();
                    playerMove.SpeedUp(_value);
                    break;
                }

            case EItemType.AttackSpeedUp:
                {
                    PlayerFire playerFire = collision.GetComponent<PlayerFire>();
                    playerFire.SpeedUp(_value);
                    break;
                }
            case EItemType.HealthUp:
                {
                    PlayerHealth player = collision.GetComponent<PlayerHealth>();
                    player.Heal(_value);
                    break;
                }
        }

        PlayerEffector playerEffector = collision.GetComponent<PlayerEffector>();
        playerEffector.InstantiateEffect(_type);
        
        if(SoundManager.IsManagerExist())
        {
            SoundManager.Instance.PlayItemSound(_type,playerEffector.transform);
        }
    }

    
}
