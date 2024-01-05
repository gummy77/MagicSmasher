using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : EnemyController
{
    public override void Update() {
        anim.SetFloat("speed", nav.velocity.magnitude);
        if (fleeing) {
            Flee();
        }
        Idle();
    }
}
