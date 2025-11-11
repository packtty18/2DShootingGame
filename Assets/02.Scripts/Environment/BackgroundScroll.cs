using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Material BackgroundMaterial;
    public float ScrollSpeed = 0.1f;

    private void Update()
    {
        Vector2 direction = Vector2.up;

        BackgroundMaterial.mainTextureOffset += direction * ScrollSpeed * Time.deltaTime;
    }
}
