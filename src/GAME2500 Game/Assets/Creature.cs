using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public int health;
    public float maxAcceleration;

    private Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        Debug.Log(rb2d);
        Debug.Log(rb2d == null);
    }

    void Update() {
        if (health <= 0) {
            Die();
        }
    }

    public void Run(Vector2 target) {
        Debug.Log(rb2d);
        if (rb2d == null) {
            rb2d = GetComponent<Rigidbody2D>();
        }
        Vector2 accelerationDirection = target;//.normalized;
        rb2d.AddForce(accelerationDirection * maxAcceleration);
    }

    protected virtual void AIStuff() {

    }

    protected virtual void Die() {

    }
}
