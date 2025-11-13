using UnityEngine;

public class ScoreManager : SimpleSingleton<ScoreManager>
{
    /*
     * 목표 : 적을 죽일때마다 점수 올리고 UI 반영
     */
    private const string _highScoreKey = "HighScore";

    private int _highScore = 0;
    private int _currentScore = 0;


    private void Start()
    {
        GetHighScoreFromSaveData();
        _currentScore = 0;

        RefreshtextUI(false);
    }

    private void GetHighScoreFromSaveData()
    {
        if (!SaveManager.IsManagerExist())
        {
            return;
        }

        SaveData data = SaveManager.Instance.GetSaveData();
        _highScore = data.HighScore;
    }

    public void AddScore(int score)
    {
        _currentScore += score;

        if (IsHighScore())
        {
            _highScore = _currentScore;
            SaveHighScore(_highScore);
        }

        RefreshtextUI();
    }

    

    private void RefreshtextUI(bool tween = true)
    {
        
        if (!UIManager.IsManagerExist())
        {
            return;

        }

        UIManager ui = UIManager.Instance;
        string changeHighText = $"최고 점수 : {_highScore.ToString("N0")}";
        ui.ChangeHighScoreText(changeHighText);

        string changeText = $"현재 점수 : {_currentScore.ToString("N0")}";
        ui.ChangeScoreText(changeText, tween);
    }

    private bool IsHighScore()
    {
        return _highScore < _currentScore;
    }


    private void SaveHighScore(int score)
    {
        if (!SaveManager.IsManagerExist())
        {
            return;
        }

        SaveManager save = SaveManager.Instance;
        SaveData data = save.GetSaveData();
        data.SetHighScore(score);
        save.Save();
    }

    [ContextMenu("ResetHighScore")]
    public void ResetScore()
    {
        SaveManager save = SaveManager.Instance;
        if (save == null)
        {
            Debug.LogError("There's No SaveManager");
            return;
        }

        save.DeleteSave();
    }
}
