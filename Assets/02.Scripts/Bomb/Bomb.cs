using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime = 3;
    private float _timer;

    private void Start()
    {
        _timer = _lifeTime;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if( _timer < 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnDead();
        }
    }
}
