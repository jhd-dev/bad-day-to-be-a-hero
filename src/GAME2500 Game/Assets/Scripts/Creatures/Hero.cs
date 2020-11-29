using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Hero : Creature {

    [SerializeField] int boneValue;
    [SerializeField] GameObject bone;
    AIDestinationSetter destinationSetter;

    void Awake() {
        InvokeRepeating("AttemptAttack", 2, 2);
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = GameObject.FindGameObjectWithTag("HeroGoal").transform;
    }

    protected override void Die() {
        DropBones();
        base.Die();
    }

    protected virtual void AttemptAttack() {
        if (attack != null) {
            attack.Attempt(this, GetClosestVillain().transform.position);
        }
    }

    GameObject GetClosestVillain() {
        List<GameObject> villains = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));
        villains.Add(GameObject.FindGameObjectWithTag("Boss"));
        Camera cam = Camera.main;
        float minDistance = 99999;
        int closestIndex = -1;

        for (int i = 0; i < villains.Count; i++) {
            Vector2 minionPos = villains[i].transform.position;
            float distance = Vector2.Distance(minionPos, transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return villains[closestIndex];
    }

    void DropBones() {
        for (int i = 0; i < boneValue; i++) {
            Instantiate(bone, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }
}
