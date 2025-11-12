using UnityEngine;

public class ScoreManager : SimpleSingleton<ScoreManager>
{
    /*
     * 목표 : 적을 죽일때마다 점수 올리고 UI 반영
     */
    private const string saveKey = "Score";
    private int _currentScore = 0;


    private void Start()
    {
        _currentScore = PlayerPrefs.GetInt(saveKey, 0);

        RefreshtextUI();
    }


    public void AddScore(int score)
    {
        _currentScore += score;

        SaveScore(_currentScore);
        RefreshtextUI();
    }

    private void RefreshtextUI()
    {
        UIManager ui = UIManager.Instance;
        if (ui == null)
        {
            Debug.LogError("There's No UIManager");
            return;

        }

        string changeText = "현재 점수 : " + _currentScore.ToString("N0");
        ui.ChangeScoreText(changeText);
    }


    //PlayerPrefs 모듈을 사용한 저장
    private void SaveScore(int score)
    {
        PlayerPrefs.SetInt(saveKey, score);
    }

    [ContextMenu("ResetScore")]
    public void ResetScore()
    {
        PlayerPrefs.SetInt(saveKey, 0);
        Debug.Log("Score is resetted");
    }
}
