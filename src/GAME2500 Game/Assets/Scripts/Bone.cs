using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    shopController shop;
    Transform soul;
    [SerializeField] int value;
    [SerializeField] float spawnForce;
    [SerializeField] int rotSpeed;
    [SerializeField] float attractionToSoul = 1;
    [SerializeField] GameObject collectionSound;
    bool collectable = false;

    void Start() {
        soul = GameObject.FindGameObjectWithTag("Soul").transform;
        shop = GameObject.FindGameObjectWithTag("Canvas").GetComponent<shopController>();
        Vector2 randomForce = new Vector2(Random.Range(-spawnForce, spawnForce), Random.Range(-spawnForce, spawnForce));
        GetComponent<Rigidbody2D>().AddForce(randomForce);
    }

    void Update() {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        float soulDistance = Vector3.Distance(transform.position, soul.position);
        transform.position = Vector3.Lerp(transform.position, soul.position, Time.deltaTime * attractionToSoul / soulDistance);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag("Soul")) {
            Instantiate(collectionSound, transform.position, Quaternion.Euler(0, 0, 0));
            shop.boneCount += value;
            Destroy(gameObject);
        }
    }
}
