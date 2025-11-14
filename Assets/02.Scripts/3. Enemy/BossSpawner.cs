using UnityEngine;

public class BossSpawner : SpanwerBase
{
    [SerializeField] private EEnemyType _type;
    [SerializeField] private int _bossSpawnScore;
    [SerializeField] private int _bossSpawnScoreOffset = 500;

    private bool isOnBoss = false;
    protected override void Init()
    {
        SetBossScore();
    }

    protected override void Spawn()
    {
        if (isOnBoss || _bossSpawnScore > ScoreManager.Instance.GetCurrentScore())
        {
            return;
        }

        EnemyFactory enemyFactory = FactoryManager.Instance.GetFactory<EnemyFactory>();
        EnemyBoss boss = enemyFactory.MakeEnemy(_type, new Vector3(0, transform.position.y, 0)).GetComponent<EnemyBoss>();
        boss.SetSpawner(this);
        isOnBoss = true;
    }

    private void SetBossScore()
    {
        _bossSpawnScore = ScoreManager.Instance.GetCurrentScore() + _bossSpawnScoreOffset;
    }

    public void ResetBossSpawn()
    {
        SetBossScore();
    }

}
