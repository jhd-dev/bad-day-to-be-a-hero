using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scanner : MonoBehaviour
{
    [SerializeField] AstarPath path;

    void Start()
    {
        InvokeRepeating("Scan", 0.5f, 0.5f);
    }

    void Scan ()
    {
        path.Scan();
    }
}
