using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Hero : Creature {

    [SerializeField] int boneValue;
    [SerializeField] GameObject bone;
    AIDestinationSetter destinationSetter;

    void Awake() {
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = GameObject.FindGameObjectWithTag("HeroGoal").transform;
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
