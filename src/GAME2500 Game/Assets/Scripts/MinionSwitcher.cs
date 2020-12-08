using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSwitcher : MonoBehaviour
{
    [SerializeField] int switchRadius = 5;
    [SerializeField] minionHealthBarScript villainListSource;
    Soul soul;

    void Start() {
        soul = GameObject.Find("Soul").GetComponent<Soul>();
    }

    void Update() {
        Villain villainOfInterest = GetClosestVillainToMouse();

        if (GetSwitchInput() && !villainOfInterest.isHost && distanceToMouse(villainOfInterest.gameObject) < switchRadius) {
            soul.SetHost(villainOfInterest);
        }
    }

    Villain GetClosestVillainToMouse() {
        float bestDistance = 9999;
        int chosenIndex = -1;

        for (int i = 0; i < villainListSource.minions.Count; i++) {
            if (distanceToMouse(villainListSource.minions[i]) < bestDistance) {
                chosenIndex = i;
                bestDistance = distanceToMouse(villainListSource.minions[i]);
            }
        }

        return villainListSource.minions[chosenIndex].GetComponent<Villain>();
    }

    float distanceToMouse(GameObject minion) {
        if (minion == null) return 9999;

        Vector3 minPos = minion.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        minPos.z = 0;
        return Vector3.Distance(minPos, mousePos);
    }

    bool GetSwitchInput()
    {
        return (MetaControl.mouseIsShoot && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))) || (!MetaControl.mouseIsShoot && Input.GetMouseButtonDown(0));
    }
}
