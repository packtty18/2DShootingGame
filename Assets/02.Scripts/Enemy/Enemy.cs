using UnityEngine;



public class Enemy : MonoBehaviour
{
    private Animator _animator;
    [Header("Type")]
    public EEnemyType Type;
    public int id;

    [Header("Stat")]
    public float Health = 100;
    public float Speed = 3;
    public float Damage = 1;

    private float _health;

    [Header("Items")]
    public GameObject[] ItemPrefabs;
    public float[] ItemWeight;

    [Header("ExplosionPrefab")]
    public GameObject[] ExplosionPrefabs;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _health = Health;
    }

    private void Update()
    {
        if (Type == EEnemyType.Direction)
        {
            MoveDirection();
        }
        else if (Type == EEnemyType.Trace)
        {
            MoveTrace();
        }
        else if (Type == EEnemyType.Teleport)
        {
            MoveDirection();
        }
    }

    private void OnDestroy()
    {
        if (EnemyObserver.Instance != null)
        {
            EnemyObserver.Instance.RemoveEnemy(id);
        }
    }

    public int GetID()
    {
        return id;
    }

    public void OnInstantiated()
    {
        EnemyObserver enemyObserver = EnemyObserver.Instance;

        if(enemyObserver == null) 
        {
            Debug.LogError("There's no Observer");
            return;
        }

        id = enemyObserver.InsertEnemy(gameObject);
    }

    private void MoveDirection()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void MoveTrace()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 playerPosition = player.transform.position;

        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;

        transform.Translate(direction * Speed * Time.deltaTime);
    }

    public void Hit(float damage)
    {
        _animator.SetTrigger("Hit");
        _health -= damage;

        if(Type == EEnemyType.Teleport && _health > 0)
        {
            float randomX = Random.Range(-2f, 2f);
            transform.position = new Vector2(randomX, transform.position.y);
        }

        if (_health <= 0)
        {
            OnDead();
        }
    }

    private void OnDead()
    {
        Debug.Log("Enemy Dead");


        //70%확률로 아이템 드롭
        float dropCheckIndex = Random.Range(0f, 1f);
        if(dropCheckIndex < 0.7f)
        {
            SpawnItem();
        }

        MakeExplosionEffect();

        Destroy(gameObject);
    }

    private void MakeExplosionEffect()
    {
        Instantiate(ExplosionPrefabs[Random.Range(0, ExplosionPrefabs.Length)], transform.position, Quaternion.identity);
    }

    private void SpawnItem()
    {
        if (ItemPrefabs == null || ItemPrefabs.Length != ItemWeight.Length)
            return;

        float totalWeight = 0f;
        foreach (float w in ItemWeight)
        {
            totalWeight += w;
        }
            

        float randomValue = Random.Range(0f, totalWeight);

        float cumulateSum= 0f; //누적합계
        int selectedIndex = 0; //선택된 아이템

        for (int i = 0; i < ItemWeight.Length; i++)
        {
            cumulateSum += ItemWeight[i];
            if (randomValue <= cumulateSum)
            {
                selectedIndex = i;
                break;
            }
        }

        // 아이템 스폰
        GameObject spawnedItem = Instantiate(ItemPrefabs[selectedIndex]);
        spawnedItem.transform.position = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) 
            return;

        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if(player == null) 
            return;

        player.Hit(Damage);

        Destroy(gameObject);
    }

    
}
