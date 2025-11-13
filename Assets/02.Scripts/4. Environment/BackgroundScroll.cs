using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 0.1f;


    private Renderer _renderer;
    private MaterialPropertyBlock _mpb;

    private Vector2 _tiling;
    private Vector2 _offset;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _mpb = new MaterialPropertyBlock();
    }

    private void Start()
    {
        Material material = _renderer.material;

        _tiling = material.mainTextureScale;
        _offset = material.mainTextureOffset;
        _mpb.SetVector("_MainTex_ST", new Vector4(_tiling.x, _tiling.y, _offset.x, _offset.y));
        _renderer.SetPropertyBlock(_mpb);
    }


    private void Update()
    {
        Vector2 direction = Vector2.up;
        _offset += direction * _scrollSpeed * Time.deltaTime;
        _mpb.SetVector("_MainTex_ST", new Vector4(_tiling.x, _tiling.y, _offset.x,_offset.y));
        _renderer.SetPropertyBlock(_mpb);
    }
}
