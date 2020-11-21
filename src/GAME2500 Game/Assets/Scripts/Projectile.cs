using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool damagesHeroes = false;
    public bool damagesVillains = false;
    public int piercable = int.MaxValue; // how many opponents the projectile can hit before being destroyed
    public float launchSpeed;
    public float lifespan;

    bool launched = false;
    Vector2 velocity = Vector2.zero;
    int piercableRemaining;
    float lifespanRemaining;
    Rigidbody2D rb2d;

    void Start() {
        piercableRemaining = piercable;
        lifespanRemaining = lifespan;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (launched) {
            if (rb2d != null) {
                rb2d.velocity = velocity;
            }
            lifespanRemaining -= Time.deltaTime;
            Debug.Log(lifespanRemaining);
            if (lifespanRemaining <= 0) {
                Destroy(this.gameObject, 1);
            }
        }
    }

    public void Launch(Vector2 direction) {
        velocity = direction.normalized * launchSpeed;
        launched = true;
    }

}
