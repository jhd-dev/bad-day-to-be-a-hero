using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buttonScript : MonoBehaviour
{

    public Enemy enemy;

    public Image artworkImage;
    public TextMeshProUGUI name;
    public TextMeshProUGUI cost;

    public GameObject dropper;

    private GameObject hud;

    private int currentBoneCount;

    // Start is called before the first frame update
    void Start()
    {
        //find hud to get bonecount info
        hud = GameObject.FindGameObjectWithTag("Canvas");
        //update bonecount
        currentBoneCount = hud.GetComponent<shopController>().boneCount;

        //display info form enemy
        name.text = enemy.name;
        cost.text = enemy.cost.ToString();
        artworkImage.sprite = enemy.artwork;
    }

    //function to be called when clicked
    public void onButtonClick()
    {
        //when button is clicked:
        // update bone count
        currentBoneCount = hud.GetComponent<shopController>().boneCount;
        //if you have enough funds, update funds and do whatever
        if (currentBoneCount >= enemy.cost)
        {
            hud.GetComponent<shopController>().boneCount = currentBoneCount - enemy.cost;
            //Instantiate whatever is that enemy's game object
            GameObject newMinion = Instantiate(dropper, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            newMinion.GetComponent<placeMinionScript>().minion = enemy.enemyGameObject;

        }
    }
}
