using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenMovingTrap : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] private float idleTime = 2f;
    [SerializeField] private ParticleSystem particalEffect;

    private int currentWaypoint = 0;
    private float timer;

    private void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if(timer <= 0.1f)
            Move();
        else
            timer -= Time.deltaTime;


        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
        {
            particalEffect.Play();
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            timer = idleTime;
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * movingSpeed);
    }
}
