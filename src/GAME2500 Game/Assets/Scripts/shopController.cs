using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopController : MonoBehaviour
{
	public GameObject shop;
	public TextMeshProUGUI bones;
	public int boneCount;

	private bool shopOpen;

	void Start()
	{
		//shop menu is equal to the ins
		shopOpen = false;
		UpdateShop();
	}

	public void TaskOnClick()
	{
		//when you click button make menu active or not
		shopOpen = !shopOpen;
		UpdateShop();
	}


	void UpdateShop()
    {
        if (shopOpen)
        {
			shop.SetActive(true);
        }
        else
        {
			shop.SetActive(false);
		}
    }

	void FixedUpdate()
    {
		bones.text = boneCount.ToString();
	}
}
