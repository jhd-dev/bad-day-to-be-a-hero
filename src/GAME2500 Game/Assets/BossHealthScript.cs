using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : MonoBehaviour
{
    private GameObject boss;
    private float initialHealth;
    private float health;
    // Start is called before the first frame update
    void Start()

    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        health = (float)boss.GetComponent<Creature>().health;
        initialHealth = boss.GetComponent<Creature>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {        
        //update health
        if (boss != null)
        {
            health = (float)boss.GetComponent<Creature>().health;
        }
        else
        {
            Destroy(gameObject);
        }

        //correct bar
        setHealthBar();

    }

    public void setHealthBar()
    {
        gameObject.transform.localScale = new Vector3((health / initialHealth), 1f, 0f);
    }
}
