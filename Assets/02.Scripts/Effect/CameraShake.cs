using UnityEngine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    [SerializeField]  private Camera _camera; //대상 -> 메인카메라
    private Vector3 _initPosition = Vector3.zero;

    [SerializeField] private float _shakeTime =1; //흔들기 시간
    [SerializeField] private Vector3 _shakeStrength = Vector3.zero; //흔드는 강도
    [SerializeField] private float _shakeDelay = 0.1f; //카메라 이동과 이동 사이의 딜레이
    [SerializeField] private AnimationCurve _shakeCurve = AnimationCurve.Linear(0, 1, 1, 0);

    private float _time;
    private float _delayTimer;

    private bool _isPlaying;

    private void Start()
    {
        _camera = Camera.main;
        _initPosition = _camera.transform.position;
        _delayTimer = _shakeDelay;
    }

    private void Update()
    {
        if(!_isPlaying)
        {
            return;
        }

        _time += Time.deltaTime;
        _delayTimer -= Time.deltaTime;

        

        if( _delayTimer < 0 )
        {
            _delayTimer = _shakeDelay;

            ShakeCamera(_time);
        }
    }

    [ContextMenu("Shake")]
    public void StartShake()
    {
        if(_isPlaying)
        {
            return;
        }

        _isPlaying = true;
        _time = 0;
        _delayTimer = _shakeDelay;
    }

    private void ShakeCamera(float time)
    {
        if (_time > _shakeTime)
        {
            EndShake();
            return;
        }

        Vector3 randomPosition = new Vector3(Random.Range(-_shakeStrength.x, _shakeStrength.x), 
            Random.Range(-_shakeStrength.y, _shakeStrength.y), 
            Random.Range(-_shakeStrength.z, _shakeStrength.z));
        _camera.transform.position = _initPosition + randomPosition * _shakeCurve.Evaluate(time/_shakeTime);
    }

    private void EndShake()
    {
        _isPlaying = false;
        _camera.transform.position = _initPosition;
    }
}
