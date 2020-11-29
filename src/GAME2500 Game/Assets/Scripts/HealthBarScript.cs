using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [HideInInspector] public GameObject minion;

    private float health;
    private float initialHealth;

    private Sprite sprite;

    private GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        // get the health of the given minion
        health = (float)minion.GetComponent<Creature>().health;

        initialHealth = (float)health;

        // get the health bar object
        healthBar = transform.GetChild(0).transform.Find("Bar").gameObject;


        //get image of enemy
        sprite = minion.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
        //set image
        transform.GetChild(1).transform.Find("CircleMask").transform.Find("Enemy Image").gameObject.GetComponent<Image>().sprite = sprite;
    }

    void setHealthBar()
    {
        healthBar.GetComponent<Transform>().localScale = new Vector3((health / initialHealth), 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //update health
        if (minion != null) {
            health = (float)minion.GetComponent<Creature>().health;
        } else {
            Destroy(gameObject);
        }

        //correct bar
        setHealthBar();
    }
}
