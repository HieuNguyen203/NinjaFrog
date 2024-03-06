using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private bool isEnter = false;
    public int life = 0;

    [SerializeField] private AudioSource death_sound;
    [SerializeField] Transform[] checkpoint;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Bullet")) && !isEnter)
        {
            isEnter = true;
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        body.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
        death_sound.Play();

    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
