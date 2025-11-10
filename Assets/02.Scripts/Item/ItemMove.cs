
using System.Xml.Serialization;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    [Header("Delay")]
    public float DelayTime = 2f;    //아이템이 생성되고 나서 움직이기 시작하는 시간
    private float _delayTimer;

    [Header("Bezier Control")]
    public float Speed = 8f;        //아이템 이동 속도
    public float BackPointY = 3f;
    public float SideMaxPointX = 2f;
    public float DestinationPointY = 3f;

    private Vector2 _startPos;
    private Vector2 _p1;
    private Vector2 _p2;
    private Vector2 _endPos;
    private float _bezierProgress;                  // 진행 정도 (0~1)

    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _delayTimer = 0;

        _startPos = transform.position;
        Vector2 directionFromPlayer = (_startPos - (Vector2)_player.transform.position).normalized;
        
        _p1 = _startPos + (directionFromPlayer * BackPointY);
        float sideMoveRate = Random.Range(-SideMaxPointX, SideMaxPointX);
        _p2 = _startPos + new Vector2(sideMoveRate, 0f);

        _endPos = _startPos + (-directionFromPlayer * DestinationPointY);
        _bezierProgress = 0f;
    }

    private void Update()
    {
        //딜레이
        if (_delayTimer < DelayTime)
        {
            _delayTimer += Time.deltaTime;
            return;
        }

        Vector2 directionFromPlayer = ((Vector2)transform.position - (Vector2)_player.transform.position).normalized;
        Vector2 _newEndPos = (Vector2)transform.position + (-directionFromPlayer * DestinationPointY);
        _endPos = Vector2.Lerp(_endPos, _newEndPos, Time.deltaTime);

        //베지어 곡선 이동
        if (_bezierProgress <= 0.9f)
        {
            _bezierProgress += Time.deltaTime;
            _bezierProgress = Mathf.Clamp01(_bezierProgress);

            Vector2 bezierPos = GetCubicBezier(_startPos, _p1, _p2, _endPos, _bezierProgress);

            transform.position =  bezierPos;
        }
        //베지어 곡선 이동 종료 후
        else
        {
            Vector2 directionToPlayer = ((Vector2)_player.transform.position - (Vector2)transform.position).normalized;
            transform.position = (Vector2)transform.position + directionToPlayer * Speed * Time.deltaTime;
        }
    }

    Vector2 GetCubicBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float progress)
    {
        Vector2 a = Vector2.Lerp(p0, p1, progress);
        Vector2 b = Vector2.Lerp(p1, p2, progress);
        Vector2 c = Vector2.Lerp(p2, p3, progress);
        Vector2 d = Vector2.Lerp(a, b, progress);
        Vector2 e = Vector2.Lerp(b, c, progress);
        return Vector2.Lerp(d, e, progress);
    }
}
