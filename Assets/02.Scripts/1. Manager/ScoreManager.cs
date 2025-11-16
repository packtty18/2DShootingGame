using System.Runtime.CompilerServices;
using UnityEngine;

public class ScoreManager : SimpleSingleton<ScoreManager>
{
    private SaveData _saveData;
    
    private int _highScore => _saveData.HighScore;
    private int _currentScore => _saveData.CurrentScore;
    private void Start()
    {
        if (!SaveManager.IsManagerExist()) 
            return;

        _saveData = SaveManager.Instance.GetSaveData();
        RefreshTextUI(false);
    }

    /// <summary>
    /// 점수 추가
    /// </summary>
    public void AddScore(int score)
    {
        int newScore = _currentScore + score;
        _saveData.SetCurrentScore(newScore); // SaveData 갱신

        //하이스코어 체크
        if (IsHighScore())
        {
            _saveData.SetHighScore(newScore);
        }

        //페이즈 업 체크
        if (_currentScore > _saveData.CurrentPhase * 1000)
        {
            int newPhase = _currentScore / 1000;
            _saveData.SetPhase(newPhase);
        }

        SaveManager.Instance.Save(); // 변경 즉시 저장
        RefreshTextUI();
    }

    /// <summary>
    /// 점수 차감 가능 여부
    /// </summary>
    public bool IsScoreCanReduce(int score)
    {
        return _currentScore - score >= 0;
    }

    /// <summary>
    /// 점수 차감
    /// </summary>
    public void ReduceScore(int score)
    {
        if(!IsScoreCanReduce(score))
        {
            return;
        }

        _saveData.SetCurrentScore(_currentScore - score);
        SaveManager.Instance.Save();
        RefreshTextUI();
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    private bool IsHighScore()
    {
        return _highScore < _currentScore;
    }

    /// <summary>
    /// UI 갱신
    /// </summary>
    private void RefreshTextUI(bool tween = true)
    {
        if (!UIManager.IsManagerExist()) return;

        var ui = UIManager.Instance;
        ui.ChangeHighScoreText($"최고 점수 : {_highScore:N0}");
        ui.ChangeScoreText($"현재 점수 : {_currentScore:N0}", tween);
    }
}
