using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SimpleSingleton<UIManager>
{
    [SerializeField] private Text _scoreTextUI;

    private void ChangeText(Text target, string text)
    {
        target.text = text;
    }

    public void ChangeScoreText(string text)
    {
        ChangeText(_scoreTextUI, text);
    }
}
