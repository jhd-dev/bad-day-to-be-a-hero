using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockerFade : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField] float fadeSpeed;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Disappear()
    {
        StartCoroutine("FadeOut");
    }

    public void Reappear()
    {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeOut()
    {
        tilemap = GetComponent<Tilemap>();

        while (tilemap.color.a > 0)
        {
            tilemap.color -= new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeIn()
    {
        while (tilemap.color.a < 1)
        {
            tilemap.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
