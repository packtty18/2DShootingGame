using System;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IPoolable>(out IPoolable poolable))
        {
            poolable.OnDeactive();
            return;
        }

        Destroy(collision.gameObject);
    }
}
