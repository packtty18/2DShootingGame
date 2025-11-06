using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 3;
    public float Damage = 1;

    private float _health = 100f;

    private void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * Speed * Time.deltaTime);
    }


    public void Hit(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Debug.Log("Enemy Dead");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) 
            return;

        Player player = collision.gameObject.GetComponent<Player>();
        if(player == null) 
            return;

        player.Hit(Damage);

        Destroy(gameObject);
    }
}
