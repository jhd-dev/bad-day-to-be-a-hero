using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack {

    public GameObject projectile;

    protected override void Execute() {
        Instantiate(projectile, transform.position, transform.rotation);
    }

}
