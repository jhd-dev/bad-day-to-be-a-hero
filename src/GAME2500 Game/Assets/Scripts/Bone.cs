using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    shopController shop;
    Transform soul;
    [SerializeField] float spawnForce;
    [SerializeField] int rotSpeed;

    void Start() {
        soul = GameObject.FindGameObjectWithTag("Soul").transform;
        shop = GameObject.FindGameObjectWithTag("Canvas").GetComponent<shopController>();
        Vector2 randomForce = new Vector2(Random.Range(-spawnForce, spawnForce), Random.Range(-spawnForce, spawnForce));
        GetComponent<Rigidbody2D>().AddForce(randomForce);
    }

    void Update() {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, soul.position, Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag("Minion") || other.gameObject.CompareTag("Boss")) {
            shop.boneCount += 10;
            Destroy(gameObject);
        }
    }
}
