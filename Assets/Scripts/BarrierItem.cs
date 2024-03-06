using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierItem : MonoBehaviour
{
    [SerializeField] private CircleCollider2D barrier;
    [SerializeField] private CircleCollider2D oldCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.GetComponent<FlyingBullet>().Kill();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.SetParent(collision.transform);
            transform.position = collision.transform.position;
            transform.localScale = new Vector2(1.75f, 1.75f);
            barrier.enabled = true;
            oldCollider.enabled = false;
        }
    }
}
