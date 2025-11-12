using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] private Text _scoreTextUI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            return;
        }
    }
    private void ChangeText(Text target, string text)
    {
        target.text = text;
    }

    public void ChangeScoreText(string text)
    {
        ChangeText(_scoreTextUI, text);
    }
}
