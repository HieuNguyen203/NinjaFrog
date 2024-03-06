using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private GameObject player;
    [SerializeField] private float sightDistance = 20f;
    [SerializeField] private float triggerTime = 5f;
    [SerializeField] private GameObject triggerIcon;
    [SerializeField] private Transform groundDetection;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask playerMask;


    private SpriteRenderer sprite;
    private Rigidbody2D rgbody;
    private Animator ani;
    private float currentTriggerTime;
    private float dirX = 1;
    private float currentMoveSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rgbody = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        currentMoveSpeed = moveSpeed;
        currentTriggerTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerDetected())
        {
            currentTriggerTime = triggerTime;
            triggerIcon.SetActive(true);
        }

        if (currentTriggerTime > 0.1f)
        {
            currentMoveSpeed = moveSpeed * 3;
            currentTriggerTime -= Time.deltaTime;
            
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 1f)
                dirX = LookForPlayer();

            if (!IsGroundDetect() || IsWallDetect())
                currentMoveSpeed = 0f;
        }
        else
        {
            if (!IsGroundDetect() || IsWallDetect())
            {
                dirX = -transform.localScale.x;
            }

            currentMoveSpeed = moveSpeed;
            triggerIcon.SetActive(false);
        }

        transform.localScale = new Vector2(dirX, 1);
        Move(transform.localScale.x);
        UpdateAnimation();
    }

    private bool IsPlayerDetected()
    {
        return Physics2D.Raycast(transform.position, (transform.localScale.x == -1) ? Vector2.left : Vector2.right, sightDistance, playerMask);
    }

    private bool IsGroundDetect()
    {
        return Physics2D.Raycast(new Vector2(groundDetection.transform.position.x, groundDetection.transform.position.y), Vector2.down, 1.5f, ground);
    }

    private bool IsWallDetect()
    {
        return Physics2D.Raycast(new Vector2(groundDetection.transform.position.x, groundDetection.transform.position.y), (transform.localScale.x == -1) ? Vector2.left : Vector2.right, 1.5f, ground);

    }

    private void Move(float dirX)
    {
        rgbody.velocity = new Vector2(dirX * currentMoveSpeed, rgbody.velocity.y);
    }

    private int LookForPlayer()
    {
        return (player.transform.position.x > transform.position.x) ? 1 : -1;         
    }

    private void UpdateAnimation()
    {
        if (currentMoveSpeed == 0f)
            ani.SetBool("IsRunning", false);
        else
            ani.SetBool("IsRunning", true);
    }
}
