using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour {

    public bool isEvil;
    public float cooldown; // in seconds
    float cooldownRemaining; // in seconds

    void Awake() {
        cooldownRemaining = 0f;
    }

    void Update() {
        cooldownRemaining -= Time.deltaTime;
    }

    public void Attempt(Creature attacker, Vector2 target) {
        if (cooldownRemaining <= 0){
            cooldownRemaining = cooldown;
            Execute(attacker, target);
        }
    }

    protected abstract void Execute(Creature attacker, Vector2 target);

};
