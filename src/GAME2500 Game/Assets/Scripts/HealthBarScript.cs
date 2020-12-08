using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [HideInInspector] public GameObject minion;

    private float health;
    private float initialHealth;
    private float oldHealth; 

    private Sprite sprite;

    private GameObject healthBar;

    private GameObject healthBarRedSprite;
    private GameObject healthBarBlueSprite;

    // Start is called before the first frame update
    void Start()
    {

        // get the health of the given minion
        health = (float)minion.GetComponent<Creature>().health;
        oldHealth = health;
        initialHealth = minion.GetComponent<Creature>().maxHealth;

        // get the health bar object
        healthBar = transform.GetChild(0).transform.Find("Bar").gameObject;

        //get the sprite objects
        healthBarRedSprite = healthBar.transform.Find("BarSprite").gameObject;
        healthBarBlueSprite = healthBar.transform.Find("BackgroundBarFlash").gameObject;

        // set the health bar initially
        setHealthBar();

        //get image of enemy
        sprite = minion.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
        //set image
        transform.GetChild(1).transform.Find("CircleMask").transform.Find("Enemy Image").gameObject.GetComponent<Image>().sprite = sprite;

        healthBarBlueSprite.SetActive(false);
        healthBarRedSprite.SetActive(true);
    }

    public void setHealthBar()
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

        //check if damage has been taken
        if(health != oldHealth)
        {
            oldHealth = health;
            StartCoroutine(flashBar());
        }
        //correct bar
        setHealthBar();

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
