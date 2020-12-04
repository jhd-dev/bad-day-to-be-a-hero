using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallAnimationController : MonoBehaviour
{
    public float lowRange;
    public float highRange;

    private float deltaTime;

    private float nextTime;
    private float nextNextTime;
    private bool increaseSize;

    private Vector3 deltaSize;

    // Start is called before the first frame update
    void Start()
    {
        deltaTime = (Random.Range(lowRange, highRange));
        deltaSize = new Vector3(0f, 0.1f, 0f);
        increaseSize = false;
        nextTime = deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            if (increaseSize)
            {
                foreach (GameObject m in GameObject.FindGameObjectsWithTag("Boss"))
                {
                    StartCoroutine(changeSize(m, increaseSize));
                }

                foreach (GameObject m in GameObject.FindGameObjectsWithTag("Minion"))
                {
                    StartCoroutine(changeSize(m, increaseSize));
                }
            }
            else
            {
                foreach (GameObject m in GameObject.FindGameObjectsWithTag("Boss"))
                {
                    StartCoroutine(changeSize(m, increaseSize));
                }

                foreach (GameObject m in GameObject.FindGameObjectsWithTag("Minion"))
                {
                    StartCoroutine(changeSize(m, increaseSize));
                }
            }
            nextTime = Time.time + (Random.Range(lowRange, highRange));
            increaseSize = !increaseSize;
        }

        
    }


    IEnumerator changeSize(GameObject m, bool yes)
    {
        yield return new WaitForSeconds(Random.Range(.05f, .25f));
        if (yes) {
            m.transform.localScale -= deltaSize;
        }
        else
        {
            m.transform.localScale += deltaSize;
        }
    }
}
