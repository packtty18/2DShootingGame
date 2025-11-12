using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SimpleSingleton<UIManager>
{
    [SerializeField] private Text _highScoreTextUI;
    [SerializeField] private Text _scoreTextUI;

    [SerializeField] private Vector3 _tweeningSize;
    [SerializeField] private float _tweenDuration;

    private void ChangeText(Text target, string text)
    {
        target.text = text;
    }

    public void ChangeHighScoreText(string text)
    {
        ChangeText(_highScoreTextUI, text);
    }

    public void ChangeScoreText(string text, bool tween = true)
    {
        ChangeText(_scoreTextUI, text);

        if(!tween)
        {
            return;
        }

        //자체제작 클래스를 사용한 방법
        //Tweening tweening = _scoreTextUI.GetComponent<Tweening>();
        //tweening.StartTweening();

        //Dotween을 사용한 방법
        _scoreTextUI.transform.DOScale(_tweeningSize, _tweenDuration).SetLoops(2, LoopType.Yoyo);
    }
}
