using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealthBarScript : MonoBehaviour
{
    [HideInInspector] public GameObject hero;

    private float health;
    private float initialHealth;

    private GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        // get the health of the given minion
        health = (float)hero.GetComponent<Hero>().GetComponent<Creature>().health;
        initialHealth = hero.GetComponent<Hero>().GetComponent<Creature>().maxHealth;

        // get the health bar object
        healthBar = transform.GetChild(0).transform.Find("Bar").gameObject;

        // set the health bar initially
        setHealthBar();

    }

    public void setHealthBar()
    {
        healthBar.GetComponent<Transform>().localScale = new Vector3((health / initialHealth), 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //update health
        if (hero != null)
        {
            health = (float)hero.GetComponent<Hero>().GetComponent<Creature>().health;
        }
        else
        {
            Destroy(gameObject);
        }

        //correct bar
        setHealthBar();
    }
}