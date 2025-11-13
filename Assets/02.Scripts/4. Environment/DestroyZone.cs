using System;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            bullet.DeActiveBullet();
            return;
        }

        Destroy(collision.gameObject);
    }
}
