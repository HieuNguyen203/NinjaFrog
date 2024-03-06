using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBullet : MonoBehaviour
{
    [SerializeField] private float fireForce;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private CapsuleCollider2D coll;
    private ParticleSystem explosionPartical;
    private AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
        explosionPartical = GetComponent<ParticleSystem>();
        explosionSound = GetComponent<AudioSource>();

        rb.velocity = transform.up * fireForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Kill();
    }

    public void Kill()
    {
        coll.enabled = false;
        sprite.enabled = false;
        explosionSound.Play();
        explosionPartical.Play();
        Destroy(gameObject, 1f);
    }
}
