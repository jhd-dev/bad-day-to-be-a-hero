using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature {

    int boneValue;

    void Start() {
        
    }

    void Update() {
        Run(Vector3.left);
    }

    protected override void Die() {
        // TODO: drop bones
        base.Die();
    }
}
