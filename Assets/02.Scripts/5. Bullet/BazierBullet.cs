using UnityEngine;

/// <summary>
/// 베지어 곡선을 통해 뒤로 이동했다가 유턴하는 총알
/// </summary>
public class BazierBullet : BulletBase
{
    [Header("Debug Bezier")]
    [Header("베지어 곡선의 제어점 변수")]
    public float DownOffset = 4f;
    public float SideOffset = 2f;
    public float DestinationOffset = 5f;

    private Vector2 _startPos;
    private Vector2 _p1;
    private Vector2 _p2;
    private Vector2 _endPos;

    private float _bezierProgress;                  // 진행 정도 (0~1)
    private bool _isBezierDone = false;
    private Vector2 _lastDir;                       // 마지막 이동 방향
    private float _bezierDuration = 1f;

    protected override void Start()
    {
        base.Start();
        //베지어 제어점 설정
        _startPos = transform.position;

        //왼쪽이냐 오른쪽이냐에 따라 다름
        if(IsLeft)
        {
            _p1 = _startPos + new Vector2(-SideOffset/2, -DownOffset);
            _p2 = _startPos + new Vector2(-SideOffset, 0f);
        }
        else
        {
            _p1 = _startPos + new Vector2(SideOffset/2, -DownOffset);
            _p2 = _startPos + new Vector2(SideOffset, 0f);
        }
        
        _endPos = _startPos + new Vector2(0f, DestinationOffset);

        _bezierProgress = 0f;
    }

    protected override Vector2 GetNewPosition()
    {
        if (!_isBezierDone)
        {
            _bezierProgress += Time.deltaTime / _bezierDuration;
            _bezierProgress = Mathf.Clamp01(_bezierProgress);

            Vector2 bezierPos = GetCubicBezier(_startPos, _p1, _p2, _endPos, _bezierProgress);

            if (_bezierProgress >= 1f)
            {
                _isBezierDone = true;
                _lastDir = (_endPos - _p2).normalized;
            }

            return bezierPos;
        }
        else
        {
            return (Vector2)transform.position + _lastDir * _speed * Time.deltaTime;
        }
    }


    //직관적 절차적 보간법
    Vector2 GetCubicBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float progress)
    {
        //1차 보간
        //현재 진행도에 따른 p0->p1, p1->p2, p2->p3로의 보간
        //원래 곡선을 1차 보간한 임시 경로
        Vector2 a = Vector2.Lerp(p0, p1, progress);
        Vector2 b = Vector2.Lerp(p1, p2, progress);
        Vector2 c = Vector2.Lerp(p2, p3, progress);

        //2차 보간
        //현재 진행도에 따라 a->b, b->c로의 보간
        //임시경로를 다시 보간한 부드러운 연결선
        Vector2 d = Vector2.Lerp(a, b, progress);
        Vector2 e = Vector2.Lerp(b, c, progress);

        //3차보간
        //현재 진행도에 따라 d->e로의 보간.
        //부드러운 연결선 위의 현재 진행도의 한 점
        return Vector2.Lerp(d, e, progress);
    }
}

