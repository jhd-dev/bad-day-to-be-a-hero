using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour {
    
    public float travelSpeed; // how fast the soul sprite moves between hosts

    Villain host; // the currently controllable enemy
    List<Villain> history; // log of previous hosts
    bool didChangeHost; // whether the host has been set during the current update cycle
    bool isTransferring; // whether the soul is visually moving between hosts
    
    void Awake() {
        Villain.soul = this;
        didChangeHost = false;
        isTransferring = false;
    }

    void Start() {
        Villain firstHost = (Boss)FindObjectOfType(typeof(Boss));
        SetHost(firstHost);
    }

    void Update() {
        if (host != null) {
            float step = travelSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, host.transform.position, step);
        }
    }

    void LateUpdate() {
        didChangeHost = false;
    }

    public void SetHost(Villain newHost) {
        if (host != newHost && !DidChangeHost()) {
            if (host != null) {
                host.BecomeHost(false);
            }
            host = newHost;
            host.BecomeHost(true);
            isTransferring = true;
            didChangeHost = true;
        }
    }

    public void RemoveFromHistory(Villain newHost) {

    }

    public bool DidChangeHost() {
        return didChangeHost;
    }
}
