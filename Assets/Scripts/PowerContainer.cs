using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerContainer : MonoBehaviour
{
    [SerializeField] private PowerUp power;
    [SerializeField] private AudioSource acquire_sound;
    private bool is_enter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !is_enter)
        {
            is_enter= true;
            power.Apply(collision.gameObject);
            acquire_sound.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
