using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntranceApple : MonoBehaviour
{
    [SerializeField] private GameObject closeWall;
    [SerializeField] private GameObject openWall;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private GameObject boss;
    [SerializeField] private WaypointFollower script;

    private bool isEnter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnter && (collision.CompareTag("Player") || collision.CompareTag("Barrier")))
        {
            isEnter = true;
            closeWall.SetActive(true);
            openWall.SetActive(false);
            bgm.Play();
            boss.SetActive(true);
            Invoke(nameof(EnableScript), 2f);
        }
    }
    private void EnableScript()
    {
        script.enabled = true;
    }
}
