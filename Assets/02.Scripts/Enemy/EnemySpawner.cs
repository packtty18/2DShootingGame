using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
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

            CoolTime = GetRandomCoolTime(MinCoolTime, MaxCoolTime);

            GameObject spawnedEnemy = Instantiate(EnemyPrefab);
            spawnedEnemy.transform.position = transform.position;
        }
    }

    private float GetRandomCoolTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}


