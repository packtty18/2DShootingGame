using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Renderer renderer;
    private MaterialPropertyBlock mpb;
    public float ScrollSpeed = 0.1f;

    public Vector2 _tiling;
    public Vector2 _offset;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        Material material = renderer.material;

        _tiling = material.mainTextureScale;
        _offset = material.mainTextureOffset;
        mpb.SetVector("_MainTex_ST", new Vector4(_tiling.x, _tiling.y, _offset.x, _offset.y));
        renderer.SetPropertyBlock(mpb);
    }

    private void Update()
    {
        Vector2 direction = Vector2.up;
        _offset += direction * ScrollSpeed * Time.deltaTime;
        mpb.SetVector("_MainTex_ST", new Vector4(_tiling.x, _tiling.y, _offset.x,_offset.y));
        renderer.SetPropertyBlock(mpb);
    }
}
