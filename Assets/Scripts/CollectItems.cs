 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    private int apples_count = 0;

    [SerializeField] private Text apple_text;
    [SerializeField] private AudioSource collect_sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Apple"))
        {
            collect_sound.Play();
            Collider2D itemCollider = collision.gameObject.GetComponent<Collider2D>();
            itemCollider.enabled = false;

            Animator ani = collision.gameObject.GetComponent<Animator>();
            ani.SetBool("collected", true);

            Destroy(collision.gameObject, 3f);

            apples_count++;
            apple_text.text = apples_count.ToString();     
        }
    }

    public int GetAppleCount()
    {
        return apples_count;
    }

    public void IncreaseAppleCount(int amount)
    {
        apples_count += amount;
        apple_text.text = apples_count.ToString();
    }

    public void DecreaseAppleCount(int amount)
    {
        apples_count -= amount;
        apple_text.text = apples_count.ToString();
    }
}
