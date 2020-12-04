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
        RefreshMinionList();
        minionCount = minions.Count;

        //if minion count is greater than max just keep it at max
        //if (minionCount > maxMinionCount)
            //minionCount = maxMinionCount;

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
        minionHealthBar.transform.localPosition = new Vector3(-300f, (300 - (100 * num)), 100);
        minionHealthBar.transform.localScale = healthBar.transform.localScale;
        return minionHealthBar;
    }

    // Update is called once per frame
    void Update()
    {
        RefreshMinionList();

        // if there was a change in the minion count (meaning a minion died or was created)
        if (minionCount != minions.Count) {
            minionCount = minions.Count;

            // if the minion count is not too large
            if (!(minionCount > maxMinionCount)) {
                foreach (GameObject healthBar in GameObject.FindGameObjectsWithTag("HealthBar")) {
                    Destroy(healthBar); // Remove existing health bars before recreating them all
                }

                for (int i = 0; i < minionCount; i++) {
                    makeHealthBar(minions[i], i);
                }
            }
        }
    }

    void RefreshMinionList() {
        minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Boss"));
        GameObject[] taggedMinions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < taggedMinions.Length; i++) {
            minions.Add(taggedMinions[i]);
        }
    }
}
