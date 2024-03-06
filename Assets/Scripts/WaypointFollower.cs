using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;

    private int current_waypoint = 0;
    
    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(transform.position, waypoints[current_waypoint].transform.position) < 0.1f)
        {
            current_waypoint++;
            if (current_waypoint >= waypoints.Length)
            {
                current_waypoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[current_waypoint].transform.position, Time.deltaTime * speed);
    }
}
