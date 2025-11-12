using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera _camera; //대상 -> 메인카메라
    private Vector3 _initPosition = Vector3.zero;
    [SerializeField] private float _shakeTime =1; //흔들기 시간
    [SerializeField] private Vector3 _shakeStrength; //흔드는 강도
    [SerializeField] private float _shakeDelay; //카메라 이동과 이동 사이의 딜레이

    private void Start()
    {
        _camera = Camera.main;
        _initPosition = _camera.transform.position;
    }
}
