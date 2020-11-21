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

    private GameObject hudCamera;

    private int currentBoneCount;

    // Start is called before the first frame update
    void Start()
    {
        //find hud to get bonecount info
        hudCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //update bonecount
        currentBoneCount = hudCamera.GetComponent<shopController>().boneCount;

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
        currentBoneCount = hudCamera.GetComponent<shopController>().boneCount;
        //if you have enough funds, update funds and do whatever
        if (currentBoneCount >= enemy.cost)
        {
            hudCamera.GetComponent<shopController>().boneCount = currentBoneCount - enemy.cost;
            //Instantiate whatever is that enemy's game object

        }
    }
}
