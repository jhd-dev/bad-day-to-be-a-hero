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
        health = minion.GetComponent<Creature>().health;

        // get the health bar object
        healthBar = transform.Find("Bar").gameObject;

        //get image of enemy
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
        //set image
        transform.Find("Enemy Image").gameObject.GetComponent<Image>().sprite = sprite;
    }

    void setHealthBar()
    {
        healthBar.transform.localScale = new Vector3((health / initialHealth), healthBar.transform.localScale.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //update health
        health = minion.GetComponent<Creature>().health;

        //correct bar
        setHealthBar();
    }
}
