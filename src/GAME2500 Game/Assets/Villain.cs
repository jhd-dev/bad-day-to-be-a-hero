using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : Creature 
{
    void FixedUpdate(){
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 accelerationDirection = new Vector2(xInput, yInput).normalized;
        rb2d.AddForce(accelerationDirection * maxAcceleration);
    }
}
