using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyingEyeBoss : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePosition;
    [SerializeField] private float fireTime;
    [SerializeField] private float posY = 10f;
    [SerializeField] private GameObject lineOfSight;
    [SerializeField] private float moveSpeed = 50f;


    private float fireTimer;
    private AudioSource shootSound;
    private Color original;

    private void Start()
    {
        fireTimer = 0;
        shootSound = GetComponent<AudioSource>();
        original = lineOfSight.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    private void Update()
    {

        fireTimer += Time.deltaTime;

        if(fireTimer > fireTime - 0.2 * fireTime)
        {
            lineOfSight.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            AimAtPlayer();
            MoveAbovePlayer();

        }

        if (fireTimer > fireTime)
        {
            fireTimer = 0;
            Shoot();
            lineOfSight.GetComponent<SpriteRenderer>().color = original;
        }
    }

    private void MoveAbovePlayer()
    {
        Vector2 abovePlayer = new Vector2(transform.position.x, player.transform.position.y + posY);
        transform.position = Vector2.MoveTowards(transform.position, abovePlayer, Time.deltaTime * moveSpeed);
    }

    private void AimAtPlayer()
    {
        Vector2 targetDirection = player.transform.position - transform.position;
        transform.up = Vector2.MoveTowards(transform.up, targetDirection, Time.deltaTime * moveSpeed);
    }

    private void Shoot()
    {
        shootSound.Play();

        Vector2 targetDirection = transform.position - firePosition.position;

        // Normalize the direction vector for a unit vector
        targetDirection.Normalize();

        // Convert normalized direction to a rotation (assuming right-handed 2D space)
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 90);

        Instantiate(bullet, firePosition.position, targetRotation);
    }
}
