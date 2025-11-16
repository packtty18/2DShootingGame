using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    // 점수 및 진행 단계
    [SerializeField] private int _highScore;           // 최고 점수
    [SerializeField] private int _currentScore;        // 현재 누적 점수
    [SerializeField] private int _currentPhase;               // 현재 진행 단계(적 강함 정도)

    // 플레이어 스탯
    [SerializeField] private float _currentPlayerHealth;        // 현재 체력
    [SerializeField] private int _currentPlayerDamageLevel;     // 공격력 업그레이드 레벨
    [SerializeField] private float _currentPlayerMoveSpeed;     // 이동 속도
    [SerializeField] private float _currentPlayerFireCooltime; // 공격 쿨타임

    // 프로퍼티로 읽기만 가능
    public int HighScore => _highScore;

    public int CurrentScore => _currentScore;
    public int CurrentPhase => _currentPhase;
    public float CurrentPlayerHealth => _currentPlayerHealth;
    public int CurrentPlayerDamageLevel => _currentPlayerDamageLevel;
    public float CurrentPlayerMoveSpeed => _currentPlayerMoveSpeed;
    public float CurrentPlayerFireCooltime => _currentPlayerFireCooltime;

    // 저장용 데이터 수정 함수 (SaveManager가 호출)
    public void SetHighScore(int score)
    {
        _highScore = score;
    }

    public void SetCurrentScore(int score)
    {
        _currentScore = score;
    }

    public void SetPhase(int phase)
    {
        _currentPhase = phase;
    }

    public void SetCurrentPlayerHealth(float hp)
    {
        _currentPlayerHealth = hp;
    }

    public void SetCurrentPlayerDamageLevel(int level)
    {
        _currentPlayerDamageLevel = level;
    }

    public void SetCurrentPlayerMoveSpeed(float speed)
    {
        _currentPlayerMoveSpeed = speed;
    }

    public void SetCurrentPlayerFireCooltime(float cooltime)
    {
        _currentPlayerFireCooltime = cooltime;
    }


    public void ResetCurrentData()
    {
        _currentScore = 0;
        _currentPhase = 1;
        _currentPlayerHealth = 3f;
        _currentPlayerDamageLevel = 0;
        _currentPlayerMoveSpeed = 3f;
        _currentPlayerFireCooltime = 0.6f;
    }   

}
