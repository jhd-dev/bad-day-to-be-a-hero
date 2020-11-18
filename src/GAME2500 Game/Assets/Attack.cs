using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour {

    public float cooldown; // in seconds
    float cooldownRemaining; // in seconds

    void Awake() {
        cooldownRemaining = 0f;
    }

    void Update() {
        cooldownRemaining -= Time.deltaTime;
    }

    public void Attempt() {
        if (cooldownRemaining <= 0){
            cooldownRemaining = cooldown;
            Execute();
        }
    }

    protected abstract void Execute();

};
