using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : MonoBehaviour
{
    private GameObject boss;
    private float initialHealth;
    private float health;
    private float oldHealth;

    private GameObject healthBarRedSprite;
    private GameObject healthBarBlueSprite;
    // Start is called before the first frame update
    void Start()

    {
        boss = GameObject.FindGameObjectWithTag("Boss");

        health = (float)boss.GetComponent<Creature>().health;
        initialHealth = boss.GetComponent<Creature>().maxHealth;
        oldHealth = health;

        healthBarRedSprite = transform.Find("HealthBar").gameObject;
        healthBarBlueSprite = transform.Find("HealthBarBlueBackground").gameObject;

        healthBarBlueSprite.SetActive(false);
        healthBarRedSprite.SetActive(true);
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

        if(oldHealth != health)
        {
            oldHealth = health;
            StartCoroutine(flashBar());
        }

        //correct bar
        setHealthBar();

    }

    public void setHealthBar()
    {
        healthBarRedSprite.transform.localScale = new Vector3((health / initialHealth), 1f, 0f);
        healthBarBlueSprite.transform.localScale = new Vector3((health / initialHealth), 1f, 0f);
    }

    IEnumerator flashBar()
    {

        healthBarBlueSprite.SetActive(true);
        healthBarRedSprite.SetActive(false);
        yield return new WaitForSeconds(.025f);

        healthBarBlueSprite.SetActive(false);
        healthBarRedSprite.SetActive(true);
    }
}
