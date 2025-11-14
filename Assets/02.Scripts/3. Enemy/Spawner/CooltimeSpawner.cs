using UnityEngine;

public class CooltimeSpawner : SpanwerBase
{
    [Header("Enemy Prefabs")]
    [SerializeField] private EEnemyType[] _spawnType;
    [SerializeField] private float[] _spawnWeight;


    //쿨타임형 스포너
    [Header("CoolTime")]
    [SerializeField] private float _coolTime = 2f;
    private float _coolTimer;

    [SerializeField] private float _minCoolTime = 1.0f;
    [SerializeField] private float _maxCoolTime = 3.0f;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        _coolTime = GetRandomCoolTime(_minCoolTime, _maxCoolTime);
    }
    
    protected override void Spawn()
    {
        _coolTimer += Time.deltaTime;

        if (_coolTimer >= _coolTime)
        {
            _coolTimer = 0f;

            //스폰할때마다 스폰시간 재설정
            _coolTime = GetRandomCoolTime(_minCoolTime, _maxCoolTime);

            if (_spawnType == null || _spawnType.Length != _spawnWeight.Length)
                return;

            float totalWeight = 0f;
            foreach (float w in _spawnWeight)
            {
                totalWeight += w;
            }


            float randomValue = Random.Range(0f, totalWeight);

            float cumulateSum = 0f; //누적합계
            int selectedIndex = 0;

            for (int i = 0; i < _spawnWeight.Length; i++)
            {
                cumulateSum += _spawnWeight[i];
                if (randomValue <= cumulateSum)
                {
                    selectedIndex = i;
                    break;
                }
            }

            if (!FactoryManager.IsManagerExist())
            {
                return;
            }

            EnemyFactory factory = FactoryManager.Instance.GetFactory<EnemyFactory>();
            factory.MakeEnemy(_spawnType[selectedIndex], transform.position);
        }
    }
   

    private float GetRandomCoolTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}


