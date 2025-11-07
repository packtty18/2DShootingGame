using UnityEngine;

/// <summary>
/// ¿·À¸·Î ÈÖ´Â ÃÑ¾Ë
/// </summary>
public class CurveBullet : BulletBase
{
    [Header("Debug Curve")]
    [Tooltip("ÈÖ´Â Å©±â")]
    public float Amplitude = 3f;  
    [Tooltip("ÈÖ´Â ÁÖ±â")]
    public float Frequency = 1f;  

    private float _time;

    protected override void Start()
    {
        base.Start();
        _time = 0f;
    }

    protected override Vector2 GetNewPosition()
    {
        _time += Time.deltaTime;

        float newY = transform.position.y + _speed * Time.deltaTime;
        float directionMultiplier = IsLeft ? -1f : 1f;

        // ÁÂ¿ì·Î SÀÚ ÀÌµ¿ (sin ÇÔ¼ö »ç¿ë)
        float newX = transform.position.x + Mathf.Sin(_time * Frequency) * Amplitude * Time.deltaTime * directionMultiplier;
        
        return new Vector2(newX, newY);
    }
}
