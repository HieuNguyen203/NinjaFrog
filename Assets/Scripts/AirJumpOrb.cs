using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUp/AirJump")]
public class AirJumpOrb : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().can_double_jump = true;
    }

}
