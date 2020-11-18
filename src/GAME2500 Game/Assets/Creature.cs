using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public int health;
    public float maxAcceleration;

    Rigidbody2D rb2d;
    Attack attack;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (health <= 0) {
            Die();
        }
    }

    public void Run(Vector2 target) {
        if (rb2d == null) {
            rb2d = GetComponent<Rigidbody2D>();
        }
        Vector2 accelerationDirection = target;//.normalized;
        rb2d.AddForce(accelerationDirection * maxAcceleration);
    }

    protected virtual void Die() {}

    protected virtual void Attack() {}

}
