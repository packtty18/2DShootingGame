using UnityEngine;


public class Tweening : MonoBehaviour
{
    [SerializeField] private float _tweenTime = 1f;
    [SerializeField] private Vector3 _targetScale = Vector3.one;

    private bool _isPlaying = false;
    private float _timer = 0f;
    private Vector3 _originScale;

    private void Start()
    {
        _originScale = transform.localScale;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _timer += Time.deltaTime;

        float halfTime = _tweenTime / 2f;
        float t;

        //절반의 시간동안 커지고 작아짐
        if (_timer < halfTime)
        {
            t = _timer / halfTime;
            transform.localScale = Vector3.Lerp(_originScale, _targetScale, t);
        }
        else if (_timer < _tweenTime)
        {
            t = (_timer - halfTime) / halfTime;
            transform.localScale = Vector3.Lerp(_targetScale, _originScale, t);
        }
        else
        {
            //완료
            transform.localScale = _originScale;
            _isPlaying = false;
            _timer = 0f;
        }
    }

    public void StartTweening()
    {
        _isPlaying = true;
        _timer = 0f;
    }
}
