using System;
using UnityEngine;

//이후 저장할 데이터들을 작성
[Serializable]
public class SaveData
{
    [SerializeField] private int _highScore;
    public int HighScore => _highScore; 
    public void SetHIghScore(int score)
    {
        _highScore = score;
    }

}
