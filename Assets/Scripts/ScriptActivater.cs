using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActivater : MonoBehaviour
{
    [SerializeField] private WaypointFollower script;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            script.enabled = true;
            Destroy(gameObject);
        }
    }
}
