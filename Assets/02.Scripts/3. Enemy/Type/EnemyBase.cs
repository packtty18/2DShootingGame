using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IPoolable
{
    private const float ITEM_DROP_RATE = 0.7f;
    [Header("Stat")] //적의 공통 스텟

    [SerializeField] private int _score = 100;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] protected float _speed = 3;
    [SerializeField] protected float _damage = 1;

    protected Transform _playerTransform;
    private Animator _animator;

    //실제 변화 스텟
    public int id;
    protected float _health;

    [Header("Items")]
    public GameObject[] ItemPrefabs;
    public float[] ItemWeight;

    [Header("ExplosionPrefab")]
    public GameObject[] ExplosionPrefabs;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Init();
    }

    public void OnActiveInit()
    {
        Init();
    }

    protected virtual void Init()
    {
        _health = _maxHealth;
        _playerTransform = GameObject.FindWithTag("Player")?.transform;

        if (!EnemyObserver.IsManagerExist())
        {
            Debug.LogError("There's no Observer");
            return;
        }

        id = EnemyObserver.Instance.InsertEnemy(gameObject);
    }

    public int GetID()
    {
        return id;
    }

    public void OnDeactive()
    {
        if (!EnemyObserver.IsManagerExist())
        {
            EnemyObserver.Instance.RemoveEnemy(id);
        }
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        OnMove();
    }

    //적 별로 움직이는 로직
    protected virtual void OnMove()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    //총알과 충돌당했을때 데미지 로직
    public virtual void OnHit(float damage)
    {
        SetHitTrigger();
        DamageLogic(damage);

        if (_health <= 0)
        {
            OnDead();
        }
    }

    private void SetHitTrigger()
    {
        _animator.SetTrigger("Hit");
    }

    protected virtual void DamageLogic(float damage)
    {
        _health -= damage;
    }

    private void OnDead()
    {
        //70%확률로 아이템 드롭
        float dropCheckIndex = Random.Range(0f, 1f);
        if (dropCheckIndex < ITEM_DROP_RATE)
        {
            SpawnItem();
        }

        MakeExplosionEffect();
        ReportScoreOnDead();
        OnDeactive();
    }

    private void ReportScoreOnDead()
    {
        if (!ScoreManager.IsManagerExist())
        {
            return;
        }

        ScoreManager.Instance.AddScore(_score);
    }

    private void MakeExplosionEffect()
    {
        GameObject effect = Instantiate(ExplosionPrefabs[Random.Range(0, ExplosionPrefabs.Length)], transform.position, Quaternion.identity);

        if (effect.TryGetComponent<CameraShake>(out CameraShake shaker))
        {
            shaker.StartShake();
        }

        if (SoundManager.IsManagerExist())
        {
            SoundManager.Instance.CreateSFX(ESFXType.Explosion, transform.position, effect.transform);
        }

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

        float cumulateSum = 0f; //누적합계
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            return;
        }

        health.Hit(_damage);
        OnDeactive();
    }
}
