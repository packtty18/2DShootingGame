using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] EnemyPrefab;
    public float[] SpawnWeight;   

    [Header("CoolTime")]
    public float CoolTime = 2f;
    private float _coolTimer;

    public float MinCoolTime = 1.0f;
    public float MaxCoolTime = 3.0f;

    private void Start()
    {
        CoolTime = GetRandomCoolTime(MinCoolTime, MaxCoolTime);
    }

    private void Update()
    {
        _coolTimer += Time.deltaTime;

        if(_coolTimer >= CoolTime)
        {
            _coolTimer = 0f;

            //스폰할때마다 스폰시간 재설정
            CoolTime = GetRandomCoolTime(MinCoolTime, MaxCoolTime);

            if (EnemyPrefab == null || EnemyPrefab.Length != SpawnWeight.Length)
                return;

            float totalWeight = 0f;
            foreach (float w in SpawnWeight)
            {
                totalWeight += w;
            }


            float randomValue = Random.Range(0f, totalWeight);

            float cumulateSum = 0f; //누적합계
            int selectedIndex = 0; //선택된 아이템

            for (int i = 0; i < SpawnWeight.Length; i++)
            {
                cumulateSum += SpawnWeight[i];
                if (randomValue <= cumulateSum)
                {
                    selectedIndex = i;
                    break;
                }
            }

            GameObject spawnedEnemy = Instantiate(EnemyPrefab[selectedIndex]);
            spawnedEnemy.transform.position = transform.position;
        }
    }

   

    private float GetRandomCoolTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}


