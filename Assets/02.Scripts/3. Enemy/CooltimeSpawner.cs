using UnityEngine;

public class CooltimeSpawner : SpanwerBase
{
    [Header("Enemy Prefabs")]
    public EEnemyType[] spawnType;
    public float[] SpawnWeight;


    //쿨타임형 스포너
    [Header("CoolTime")]
    public float CoolTime = 2f;
    private float _coolTimer;

    public float MinCoolTime = 1.0f;
    public float MaxCoolTime = 3.0f;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        CoolTime = GetRandomCoolTime(MinCoolTime, MaxCoolTime);
    }
    
    protected override void Spawn()
    {
        _coolTimer += Time.deltaTime;

        if (_coolTimer >= CoolTime)
        {
            _coolTimer = 0f;

            //스폰할때마다 스폰시간 재설정
            CoolTime = GetRandomCoolTime(MinCoolTime, MaxCoolTime);

            if (spawnType == null || spawnType.Length != SpawnWeight.Length)
                return;

            float totalWeight = 0f;
            foreach (float w in SpawnWeight)
            {
                totalWeight += w;
            }


            float randomValue = Random.Range(0f, totalWeight);

            float cumulateSum = 0f; //누적합계
            int selectedIndex = 0;

            for (int i = 0; i < SpawnWeight.Length; i++)
            {
                cumulateSum += SpawnWeight[i];
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
            factory.MakeEnemy(spawnType[selectedIndex], transform.position);
        }
    }
   

    private float GetRandomCoolTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}


