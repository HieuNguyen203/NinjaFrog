using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PowerUp/Dash")]
public class DashOrb : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().can_dash = true;
    }

}
