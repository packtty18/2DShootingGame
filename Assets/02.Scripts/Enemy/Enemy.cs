using UnityEngine;



public class Enemy : MonoBehaviour
{
    private const float SPRITE_ROTATION_OFFSET = 90f;
    private Transform _playerTransform;
    private Animator _animator;

    [Header("Type")]
    [SerializeField] private EEnemyType _type;
    public int id;

    [Header("Stat")]
    [SerializeField] private int _score = 100;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _damage = 1;

    private float _health;


    

    [Header("Items")]
    public GameObject[] ItemPrefabs;
    public float[] ItemWeight;

    [Header("ExplosionPrefab")]
    public GameObject[] ExplosionPrefabs;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _health = _maxHealth;
        _playerTransform = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (_type == EEnemyType.Direction)
        {
            MoveDirection();
        }
        else if (_type == EEnemyType.Trace)
        {
            if(_playerTransform == null)
            {
                MoveDirection();
            }
            else
            {
                MoveTrace();
            }
        }
        else if (_type == EEnemyType.Teleport)
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
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void MoveTrace()
    {
        Vector2 direction = ((Vector2)_playerTransform.transform.position - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + SPRITE_ROTATION_OFFSET;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;

        transform.position += (Vector3)(direction * _speed * Time.deltaTime);
    }

    public void Hit(float damage)
    {
        _animator.SetTrigger("Hit");
        _health -= damage;

        if(_type == EEnemyType.Teleport && _health > 0)
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
        //70%확률로 아이템 드롭
        float dropCheckIndex = Random.Range(0f, 1f);
        if (dropCheckIndex < 0.7f)
        {
            SpawnItem();
        }

        MakeExplosionEffect();
        ReportScoreOnDead();
        Destroy(gameObject);
    }

    private void ReportScoreOnDead()
    {
        if (ScoreManager.Instance == null)
        {
            return;
        }
        ScoreManager.Instance.AddScore(_score);
    }

    private void MakeExplosionEffect()
    {
        GameObject effect = Instantiate(ExplosionPrefabs[Random.Range(0, ExplosionPrefabs.Length)], transform.position, Quaternion.identity);
        CameraShake shaker = effect.GetComponent<CameraShake>();
        if(shaker == null)
        {
            return;
        }

        shaker.StartShake();
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

        player.Hit(_damage);

        Destroy(gameObject);
    }

    
}
