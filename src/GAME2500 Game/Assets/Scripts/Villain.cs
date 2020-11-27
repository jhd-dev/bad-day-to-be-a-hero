using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Villain : Creature {

    public static Soul soul;

    protected bool isHost = false; // whether this is the currently controllable enemy
    const KeyCode swapHostKey = KeyCode.E; // the key to be pressed to switch hosts
    const KeyCode attackKey = KeyCode.Space; // the key to be pressed to switch hosts

    void Update() {
        if (isHost) {
            HandleMovementInput();
            HandleHostSwapInput();
            HandleAttackInput();
        } else {
            // AI Stuff
        }
    }

    public void BecomeHost(bool isBecoming = true) {
        isHost = isBecoming;
    }

    public void SwapToHost(Villain next) {
        soul.SetHost(next);
    }

    protected override void Die() {
        SwapToNextHost();
        soul.RemoveFromHistory(this);
        base.Die();
    }

    protected virtual void HandleAttackInput() {
        if (Input.GetKeyDown(attackKey)) {
            Vector2 mouseScreenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            attack.Attempt(this, mouseWorldPosition);
        }
    }

    void HandleMovementInput() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Run(new Vector2(xInput, yInput));
    }

    void HandleHostSwapInput() {
        if (soul != null && !soul.DidChangeHost() && Input.GetKeyDown(swapHostKey) && !ControlCenter.inCameraMode) {
            SwapToNextHost();
        }
    }

    void SwapToNextHost() {
        List<Villain> villains = GetVillainsByDistance();
        if (villains.Count > 1) {
            SwapToHost(villains[1]);
        }
    }

    void SwapToPrevHost() {
        List<Villain> villains = GetVillainsByDistance();
        SwapToHost(villains[villains.Count - 1]);
    }

    List<Villain> GetVillainsByDistance() {
        List<Villain> villains = GameObject.FindObjectsOfType<Villain>().ToList<Villain>();
        List<Villain> closest = villains.OrderBy(
            v => Vector2.Distance(this.transform.position, v.transform.position)
        ).ToList();
        return closest;
    }

}
