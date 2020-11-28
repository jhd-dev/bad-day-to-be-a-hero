using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public int maxHealth;
    public int health { get; private set; }
    public float maxAcceleration;

    public Attack attack;
    public SpriteRenderer spriteRenderer;

    Rigidbody2D rb2d;

    void Start() {
        if (GetComponent<Rigidbody2D>() != null) {
            rb2d = GetComponent<Rigidbody2D>();
        }
        spriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    public void Run(Vector2 target) {
        if (rb2d == null) {
            rb2d = GetComponent<Rigidbody2D>();
        }

        Vector2 accelerationDirection = target;//.normalized;
        rb2d.AddForce(accelerationDirection * maxAcceleration);
    }

    public virtual void TakeDamage(int damage, bool directHit = true) { // damage: the amount of damage; directHit: whether the damage is from an attack, or else something different (i.e. poison or an AoE)
        if (directHit) {
            // play sound? visual effect?
        }
        health -= damage;
        StartCoroutine("AnimateDamage");
        if (health <= 0) {
            Die();
        }
    }

    IEnumerator AnimateDamage()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }

    protected virtual void Die() {
        Destroy(this.gameObject, 0);
    }
}
