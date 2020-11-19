using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack {

    public Projectile projectile;

    protected override void Execute() {
        Projectile p = Instantiate(projectile, transform.position, transform.rotation) as Projectile;
        p.Launch(new Vector2(1, 1));
    }

}
