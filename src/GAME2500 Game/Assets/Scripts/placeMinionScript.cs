using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeMinionScript : MonoBehaviour
{
    [HideInInspector] public GameObject minion;
    bool onBadGround;

    // Start is called before the first frame update
    void Start()
    {
        // set image
        gameObject.GetComponent<SpriteRenderer>().sprite = minion.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        //keep image on mouse
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //on click place minion and destroy gameobject
        if (Input.GetMouseButtonDown(0) && !onBadGround)
        {
            Instantiate(minion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // This gameobject can only collide with "bad ground" (dirt, outdoors)
    void OnTriggerStay2D(Collider2D collider)
    {
        onBadGround = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        onBadGround = false;
    }
}
