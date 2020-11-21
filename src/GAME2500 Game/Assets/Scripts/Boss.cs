using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Villain {
    
    public int boneCount;

    void Start() {
        //Villain.boss = this;
        if (Villain.soul != null) {
            Villain.soul.SetHost(this);
        }
    }

    protected override void Die() {
        System.Console.WriteLine("Game Over");
    }
}
