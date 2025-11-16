using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EuttonType
{
    Attack,
    Bomb,
    DamageUp
}   

public class UIManager : SimpleSingleton<UIManager>
{
    [SerializeField] private Text _highScoreTextUI;
    [SerializeField] private Text _scoreTextUI;

    

    [SerializeField] private Vector3 _tweeningSize;
    [SerializeField] private float _tweenDuration;

    [Header("Input")]
    [SerializeField] private DynamicJoystick _joyStick;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _bombButton;
    [SerializeField] private Button _damageUpButton;

    private Dictionary<EuttonType, Button> _buttonDictionary = new Dictionary<EuttonType, Button>();

    protected override void Awake()
    {
        base.Awake();
        MatchButtonEnum();
    }

    private void MatchButtonEnum()
    {
        _buttonDictionary.Add(EuttonType.Attack, _attackButton);
        _buttonDictionary.Add(EuttonType.Bomb, _bombButton);
        _buttonDictionary.Add(EuttonType.DamageUp, _damageUpButton);
    }

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
        Tweening tweening = _scoreTextUI.GetComponent<Tweening>();
        tweening.StartTweening();

        //Dotween을 사용한 방법
        //_scoreTextUI.transform.DOScale(_tweeningSize, _tweenDuration).SetLoops(2, LoopType.Yoyo);
    }

    public DynamicJoystick GetJoystick()
    {
        return _joyStick;
    }
    public Button GetButton(EuttonType buttonType)
    {
        return _buttonDictionary[buttonType];
    }

}
