using UnityEngine;

public class BossSpawner : SpanwerBase
{
    [SerializeField] private EEnemyType _type;
    [SerializeField] private int _bossSpawnScore;
    [SerializeField] private int _bossSpawnScoreOffset = 500;

    private bool _isOnBoss = false;

    protected override void Initialize()
    {
        SetBossScore();
    }

    protected override void Spawn()
    {
        if(!ScoreManager.IsManagerExist())
        {
            return;
        }

        if (_isOnBoss || _bossSpawnScore > ScoreManager.Instance.GetCurrentScore())
        {
            return;
        }

        EnemyFactory enemyFactory = FactoryManager.Instance.GetFactory<EnemyFactory>();
        EnemyBoss boss = enemyFactory.MakeEnemy(_type, new Vector3(0, transform.position.y, 0)).GetComponent<EnemyBoss>();
        boss.SetSpawner(this);
        _isOnBoss = true;
    }

    private void SetBossScore()
    {
        if (!SaveManager.IsManagerExist())
        {
            return;
        }

        _bossSpawnScore = SaveManager.Instance.GetSaveData().CurrentScore + _bossSpawnScoreOffset;
    }

    public void ResetBossSpawn()
    {
        SetBossScore();
    }

}
