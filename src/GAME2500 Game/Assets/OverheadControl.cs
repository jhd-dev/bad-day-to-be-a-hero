using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadControl : MonoBehaviour
{
    enum ZoomType
    {
        In,
        Out,
        OnMinion
    }

    Camera cam;
    [Header("Pivoting")]
    [SerializeField] float defaultHorizSpeed;
    [SerializeField] float defaultVertSpeed;
    [Header("Zooming")]
    [SerializeField] int minimumZoomLevel;
    [SerializeField] int maximumZoomLevel;
    [SerializeField] float zoomSpeed;
    float actualHorizSpeed;
    float actualVertSpeed;
    int zoomLevel = 0;
    bool inZoomSequence;
    GameObject[] minions;

    void Start()
    {
        minions = GameObject.FindGameObjectsWithTag("Minion");
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Pivot();
        ConsiderPossession();
        ConsiderZoom();
    }

    void Pivot()
    {
        actualHorizSpeed = defaultHorizSpeed * Mathf.Pow(2, zoomLevel);
        actualVertSpeed = defaultVertSpeed * Mathf.Pow(2, zoomLevel);
        float x = Input.GetAxis("CamHorizontal") * actualHorizSpeed;
        float y = Input.GetAxis("CamVertical") * actualVertSpeed;
        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }

    void ConsiderPossession()
    {
        GameObject minion = GetClosestMinion();

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            StartCoroutine("Zoom", ZoomType.OnMinion);
        }
    }

    GameObject GetClosestMinion()
    {
        Vector2 midScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        float minDistance = 99999;
        int closestIndex = 0;

        for (int i = 0; i < minions.Length; i++)
        {
            Vector2 minionPos = cam.WorldToScreenPoint(minions[i].transform.position);
            float distance = Vector2.Distance(minionPos, midScreen);
            print(distance);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return minions[closestIndex];
    }

    void ConsiderZoom()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !inZoomSequence)
        {
            StartCoroutine("Zoom", ZoomType.In);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !inZoomSequence)
        {
            StartCoroutine("Zoom", ZoomType.Out);
        }
    }

    IEnumerator Zoom (ZoomType zoomType)
    {
        // Setup

        inZoomSequence = true;
        float goalZoom = cam.orthographicSize;

        switch (zoomType)
        {
            case ZoomType.In:
                if (zoomLevel != minimumZoomLevel)
                {
                    goalZoom = cam.orthographicSize / 2;
                    zoomLevel--;
                }
                else
                {
                    StopCoroutine("Zoom");
                }
                break;
            case ZoomType.Out:
                if (zoomLevel != maximumZoomLevel)
                {
                    goalZoom = cam.orthographicSize * 2;
                    zoomLevel++;
                }
                else
                {
                    StopCoroutine("Zoom");
                }
                break;
            default:
                goalZoom = 4;
                //todo: Something about zoomLevel
                break;
        }

        //Animation

        while (Mathf.Abs(cam.orthographicSize - goalZoom) > 0.5f)
        {
            cam.orthographicSize += (goalZoom - cam.orthographicSize) / (8 / zoomSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        inZoomSequence = false;
    }
}
