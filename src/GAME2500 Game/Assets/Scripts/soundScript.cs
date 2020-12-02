using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundScript : MonoBehaviour
{
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!aud.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
