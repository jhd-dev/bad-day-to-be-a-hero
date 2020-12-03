using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool damagesHeroes = false;
    public bool damagesVillains = false;
    public int piercable = int.MaxValue; // how many opponents the projectile can hit before being destroyed
    public float launchSpeed;
    public float lifespan;
    public int power; // amount of damage to do
    [SerializeField] bool passesThroughWalls;
    [SerializeField] bool canceledByOtherProjectiles;
    [SerializeField] GameObject wallHitSound;

    bool launched = false;
    Vector2 velocity = Vector2.zero;
    int piercableRemaining;
    float lifespanRemaining;
    Rigidbody2D rb2d;
    List<int> collidedWith = new List<int>(); // filled with Instance IDs

    void Start() {
        SetRotation();
        piercableRemaining = piercable;
        lifespanRemaining = lifespan;
    }

    void SetRotation()
    {
        float angle = 180 + (Mathf.Rad2Deg * Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x));
        transform.eulerAngles = new Vector3 (0, 0, angle);
    }

    void Update() {
        if (launched) {
            if (rb2d != null) {
               // rb2d.velocity = velocity;
            }
            lifespanRemaining -= Time.deltaTime;
            if (lifespanRemaining <= 0) {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (
            col != null
            && col.gameObject != null
            && !collidedWith.Contains(col.gameObject.GetInstanceID())
            && (((col.gameObject.CompareTag("Minion") || col.gameObject.CompareTag("Boss")) && damagesVillains)
                || (col.gameObject.CompareTag("Hero") && damagesHeroes))
        ) {
            col.gameObject.GetComponent<Creature>().TakeDamage(power);
            collidedWith.Add(col.gameObject.GetInstanceID());
            piercableRemaining --;
            if (piercableRemaining < 0) {
                Destroy(this.gameObject);
            }
        }

        if (col != null && col.gameObject.CompareTag("Wall") && !passesThroughWalls)
        {
            Instantiate(wallHitSound, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(this.gameObject);
        }

        // Projectiles cancel each other out
        if (col != null && col.gameObject.CompareTag("Projectile") && canceledByOtherProjectiles)
        {
            Instantiate(wallHitSound, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 direction, bool isEvil) {
        if (rb2d == null) {
            rb2d = GetComponent<Rigidbody2D>();
        }
        rb2d.velocity = direction.normalized * launchSpeed;
        launched = true;
        damagesHeroes = isEvil;
        damagesVillains = !isEvil;
    }

}
