using UnityEngine;

public class BombBullet : MonoBehaviour
{
    //현재 위치에서 0,0,0으로 이동
    [SerializeField]
    private GameObject _bombPrefab;
    [SerializeField]
    private GameObject _effect;
    [SerializeField]
    private Vector2 _targetPos = Vector2.zero;
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _threshold = 0.1f;
    private Vector2 _direction;


    private void Start()
    {
        _direction = (_targetPos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;
    }


    private void Update()
    {
        if(Vector2.Distance(_targetPos, transform.position) < _threshold)
        {
            Instantiate(_effect, _targetPos, Quaternion.identity);
            Instantiate(_bombPrefab, _targetPos, Quaternion.identity);
            Destroy(gameObject);
        }
        transform.position = (Vector2)transform.position + _direction * _moveSpeed*Time.deltaTime;
    }
}
