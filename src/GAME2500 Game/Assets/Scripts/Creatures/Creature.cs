using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public int maxHealth;
    public int health { get; private set; }
    public float maxSpeed;

    public Attack attack;
    SpriteRenderer spriteRenderer;
    Color defaultSpriteColor;

    Rigidbody2D rb2d;

    [SerializeField] GameObject damageSound;
    [SerializeField] GameObject deathSound;

    void Start() {
        if (GetComponent<Rigidbody2D>() != null) {
            rb2d = GetComponent<Rigidbody2D>();
        }
        spriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        defaultSpriteColor = spriteRenderer.color;
        health = maxHealth;
    }

    public void Run(Vector2 target) {
        if (rb2d == null) {
            rb2d = GetComponent<Rigidbody2D>();
        }

        Vector2 direction = target;//.normalized;
        //rb2d.AddForce(accelerationDirection * maxAcceleration);
        rb2d.velocity = (direction * maxSpeed);
    }

    public virtual void TakeDamage(int damage, bool directHit = true) { // damage: the amount of damage; directHit: whether the damage is from an attack, or else something different (i.e. poison or an AoE)

        if (directHit) {
            // play sound? visual effect?
        }
        health -= damage;
        StartCoroutine("AnimateDamage");
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Instantiate(damageSound, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    IEnumerator AnimateDamage() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = defaultSpriteColor;
    }

    protected virtual void Die() {
        Instantiate(deathSound, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(this.gameObject, 0);
    }

}
