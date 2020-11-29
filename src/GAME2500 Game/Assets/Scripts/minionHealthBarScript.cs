using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionHealthBarScript : MonoBehaviour
{
    public GameObject healthBar;
    public int maxMinionCount;


    private List<GameObject> minions = new List<GameObject>();
    private int minionCount;


    // Start is called before the first frame update
    void Start()
    {
        minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));
        minions.Add(GameObject.FindGameObjectWithTag("Boss"));
        minionCount = minions.Count;

        //if minion count is greater than max just keep it at max
        if (minionCount > maxMinionCount)
            minionCount = maxMinionCount;

        for (int i = 0; i < minionCount; i++)
        {
            makeHealthBar(minions[i], i);
        }
    }

    private GameObject makeHealthBar(GameObject gO, int num)
    {
        GameObject minionHealthBar = Instantiate(healthBar);
        minionHealthBar.GetComponent<HealthBarScript>().minion = gO;
        minionHealthBar.transform.SetParent(gameObject.transform);
        minionHealthBar.transform.position = new Vector3(-6f, (0 - (0.75f * num)), 100);
        return minionHealthBar;
    }

    // Update is called once per frame
    void Update()
    {
        minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));

        // if there are less than max minions and the count is not equal to the previous count (meaning a minion died or was created)
        if (!(minionCount > maxMinionCount) && (minionCount != minions.Count))
        {
            minionCount = minions.Count;
            // check to see if updated list is greater than max
            if (minionCount > maxMinionCount)
                minionCount = maxMinionCount;

            for (int i = 0; i < minionCount; i++)
            {
                makeHealthBar(minions[i], i);
            }
        }
        
    }
}
