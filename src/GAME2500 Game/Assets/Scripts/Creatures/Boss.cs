using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : Villain {

    public float attackRadius;
    public float attackRate;

    private AIDestinationSetter destinationSetter;
    private AIPath path;

    private Vector3 p1;
    private Vector3 p2;
    private GameObject target;

    private float nextTargetTime;
    private GameObject targetArea; 

    public int boneCount;

    void Awake() {
        targetArea = GameObject.FindGameObjectWithTag("Target");
        p1 = new Vector3(targetArea.transform.position.x - (targetArea.transform.localScale.x / 2), targetArea.transform.position.y - (targetArea.transform.localScale.y / 2), 0f);
        p2 = new Vector3(targetArea.transform.position.x + (targetArea.transform.localScale.x / 2), targetArea.transform.position.y + (targetArea.transform.localScale.y / 2), 0f);

        target = new GameObject("target");
        target.transform.parent = targetArea.transform;
        target.transform.position = new Vector3(Random.Range(p1.x, p2.x), Random.Range(p1.y, p2.y), 0f);
        Instantiate(target);
        nextTargetTime = 1f;

        destinationSetter = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
        destinationSetter.enabled = false;
        path.enabled = false;

        InvokeRepeating("AttemptAttack", attackRate, attackRate);
        destinationSetter.target = target.transform;


        destinationSetter.target = target.transform;
        //Villain.boss = this;
        if (Villain.soul != null) {
            Villain.soul.SetHost(this);
        }
    }

    protected override void Die() {
        GameStatus.isGameOver = true;
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Boss>().isHost)
        {
            destinationSetter.enabled = true;
            path.enabled = true;
            if (Time.time > nextTargetTime)
            {
                target.transform.position = new Vector3(Random.Range(p1.x, p2.x), Random.Range(p1.y, p2.y), 0f);
                nextTargetTime = Time.time + 1f;
            }
        }
        else
        {
            destinationSetter.enabled = false;
            path.enabled = false;
        }
    }

    protected virtual void AttemptAttack()
    {
        if (!gameObject.GetComponent<Villain>().isHost && attack != null && (GameObject.FindGameObjectsWithTag("Hero").Length > 0))
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
