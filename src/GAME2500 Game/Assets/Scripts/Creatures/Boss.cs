using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Villain {
    
    public int boneCount;

    void Awake() {
        //Villain.boss = this;
        if (Villain.soul != null) {
            Villain.soul.SetHost(this);
        }
    }

    protected override void Die() {
        GameStatus.isGameOver = true;
        Destroy(gameObject);
    }
}
