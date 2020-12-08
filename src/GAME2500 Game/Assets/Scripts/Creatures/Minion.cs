using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Minion : Villain {

    public float attackRadius;
    public float attackRate;

    private AIDestinationSetter destinationSetter;
    private AIPath path;

    private Vector3 p1;
    private Vector3 p2;
    private GameObject target;

    private float nextTargetTime;
    [SerializeField] GameObject targetArea;
    GameObject myTargetArea;

    void Awake()
    {
        myTargetArea = Instantiate(targetArea, transform.position, Quaternion.Euler(0, 0, 0));
        target = new GameObject("target");
        Instantiate(target, myTargetArea.transform);
        SetTarget();

        destinationSetter = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
        destinationSetter.enabled = false;
        path.enabled = false;

        InvokeRepeating("AttemptAttack", attackRate, attackRate);
        destinationSetter.target = target.transform;
    }

    void FixedUpdate()
    {
        myTargetArea.transform.position = transform.position;

        if (!gameObject.GetComponent<Minion>().isHost)
        {
            destinationSetter.enabled = true;
            path.enabled = true;
            if (Time.time > nextTargetTime)
            {
                SetTarget();
            }
        }
        else
        {
            destinationSetter.enabled = false;
            path.enabled = false;
        }
    }

    void SetTarget()
    {
        p1 = new Vector3(myTargetArea.transform.position.x - (myTargetArea.transform.localScale.x / 2), myTargetArea.transform.position.y - (myTargetArea.transform.localScale.y / 2), 0f);
        p2 = new Vector3(myTargetArea.transform.position.x + (myTargetArea.transform.localScale.x / 2), myTargetArea.transform.position.y + (myTargetArea.transform.localScale.y / 2), 0f);
        target.transform.position = new Vector3(Random.Range(p1.x, p2.x), Random.Range(p1.y, p2.y), 0f);
        nextTargetTime = Time.time + 1f;
        nextTargetTime = 1f;
    }

    protected virtual void AttemptAttack()
    {
        if (!gameObject.GetComponent<Minion>().isHost && attack != null && (GameObject.FindGameObjectsWithTag("Hero").Length > 0))
        {
            GameObject closestVillain = GetClosestVillain();
            if (Vector2.Distance(transform.position, closestVillain.transform.position) < attackRadius)
            {
                attack.Attempt(this, closestVillain.transform.position);
            }
        }
    }


    GameObject GetClosestVillain()
    {
        List<GameObject> heroes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hero"));
        Camera cam = Camera.main;
        float minDistance = 99999;
        int closestIndex = -1;

        for (int i = 0; i < heroes.Count; i++)
        {
            Vector2 heroPos = heroes[i].transform.position;
            float distance = Vector2.Distance(heroPos, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return heroes[closestIndex];
    }
}
