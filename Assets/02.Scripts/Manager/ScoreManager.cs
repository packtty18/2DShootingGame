using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /*
     * 목표 : 적을 죽일때마다 점수 올리고 UI 반영
     */
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            return instance;
        }
    }

    private int _currentScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        _currentScore = 0;
        string changeText = "현재 점수 : 0";

        RefreshUI(changeText);
    }


    public void AddScore(int score)
    {
        _currentScore += score;
        string changeText = "현재 점수 : " + _currentScore.ToString();

        RefreshUI(changeText);
    }

    private void RefreshUI(string text)
    {
        UIManager ui = UIManager.Instance;
        if (ui == null)
        {
            Debug.LogError("There's No UIManager");
            return;
        }

        ui.ChangeScoreText(text);
    }
}
