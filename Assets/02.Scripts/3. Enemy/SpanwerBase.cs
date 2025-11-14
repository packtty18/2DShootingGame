using UnityEngine;

public abstract class SpanwerBase : MonoBehaviour
{
    
    private void Update()
    {
        Spawn();
    }

    protected abstract void Init();
    protected abstract void Spawn();
}
