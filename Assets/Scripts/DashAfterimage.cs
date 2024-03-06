using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterimage : MonoBehaviour
{
    [SerializeField] private float ghost_delay;
    [SerializeField] private GameObject ghost;
    public bool create_afterimage = false;
    private float ghost_delay_seconds;
    
    // Start is called before the first frame update
    private void Start()
    {
        ghost_delay_seconds = ghost_delay;
    }

    // Update is called once per frame
    private void Update()
    {
        if (create_afterimage)
        {
            if (ghost_delay_seconds > 0)
            {
                ghost_delay_seconds -= Time.deltaTime;
            }
            else
            {
                //generating a ghost
                GameObject current_ghost = Instantiate(ghost, transform.position, transform.rotation);
                SpriteRenderer current_sprite = GetComponent<SpriteRenderer>();
                current_ghost.GetComponent<SpriteRenderer>().sprite = current_sprite.sprite;
                current_ghost.GetComponent<SpriteRenderer>().flipX = current_sprite.flipX;
                ghost_delay_seconds = ghost_delay;
                Destroy(current_ghost, 1f);
            }
        }
    }
}
