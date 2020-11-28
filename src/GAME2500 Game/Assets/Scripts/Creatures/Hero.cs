using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature {

    [SerializeField] int boneValue;
    [SerializeField] GameObject bone;

    void Start() {
        
    }

    void Update() {
        //Run(Vector3.left);
    }

    protected override void Die() {
        DropBones();
        base.Die();
    }

    void DropBones() {
        for (int i = 0; i < boneValue; i++) {
            Instantiate(bone, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }
}
