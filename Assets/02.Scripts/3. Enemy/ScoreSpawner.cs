using UnityEngine;

public class ScoreSpawner : SpanwerBase
{
    [SerializeField] private EEnemyType _type;
    private int _bossSpawnScore;
    [SerializeField] private int _bossSpawnScoreOffset = 500;

    private void Start()
    {
        
    }

    protected override void Init()
    {
        SetBossScore();
    }

    protected override void Spawn()
    {
        if (_bossSpawnScore > ScoreManager.Instance.GetCurrentScore())
        {
            return;
        }

        EnemyFactory enemyFactory = FactoryManager.Instance.GetFactory<EnemyFactory>();
        enemyFactory.MakeEnemy(_type, new Vector3(0, transform.position.y, 0));
        SetBossScore();
    }

    private void SetBossScore()
    {
        _bossSpawnScore = ScoreManager.Instance.GetCurrentScore() + _bossSpawnScoreOffset;
    }

}
