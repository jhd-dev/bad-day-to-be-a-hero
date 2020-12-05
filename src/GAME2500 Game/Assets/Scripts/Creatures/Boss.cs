using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : Villain {
    
    private AIDestinationSetter destinationSetter;
    private AIPath path;

    private GameObject target;

    public int boneCount;

    void Awake() {

        destinationSetter = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("HeroGoal");

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
        }
        else
        {
            destinationSetter.enabled = false;
            path.enabled = false;
        }
    }

}
