using UnityEngine;

public abstract class SpanwerBase : MonoBehaviour
{
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        Spawn();
    }

    protected abstract void Initialize();
    protected abstract void Spawn();
}
