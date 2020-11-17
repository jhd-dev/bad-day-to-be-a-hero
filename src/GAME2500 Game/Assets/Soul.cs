using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour {
    
    private Villain host; // the currently controllable enemy
    private bool isTransferring; // whether the soul is visually moving between hosts
    private List<Villain> history; // log of previous hosts
    private bool didChangeHost;
    
    void Awake() {
        Villain.soul = this;
        didChangeHost = false;
        isTransferring = false;
    }

    void Start() {
        Villain firstHost = (Boss)FindObjectOfType(typeof(Boss));
        SetHost(firstHost);
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
