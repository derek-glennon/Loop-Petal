using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : Platform {
    public bool isActive;

    public override void Activate()
    {
        isActive = !isActive;

        boxCollider.enabled = isActive;
        animator.SetBool("Active", isActive);
    }
}
