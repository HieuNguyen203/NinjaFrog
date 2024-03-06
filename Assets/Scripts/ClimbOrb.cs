using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUp/ClimbOrb")]
public class ClimbOrb : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().can_wall_jump = true;
    }

}
