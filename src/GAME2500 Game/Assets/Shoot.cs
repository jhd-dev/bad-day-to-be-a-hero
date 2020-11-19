using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack {

    public Projectile projectile;

    protected override void Execute(Creature attacker, Vector2 target) {
        Projectile p = Instantiate(projectile, transform.position, transform.rotation) as Projectile;
        p.Launch(target - (Vector2)transform.position, isEvil);
    }

}
