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

    //Serialized Attributes
    Camera cam;
    [Header("Pivoting")]
    [SerializeField] float defaultHorizSpeed;
    [SerializeField] float defaultVertSpeed;
    [Header("Zooming")]
    [SerializeField] int minimumZoomLevel;
    [SerializeField] int maximumZoomLevel;
    [SerializeField] float zoomSpeed;

    // Other Attributes
    float actualHorizSpeed;
    float actualVertSpeed;
    float defaultZoom;
    int zoomLevel = 0;
    bool inZoomSequence;
    GameObject[] minions;

    void Start()
    {
        minions = GameObject.FindGameObjectsWithTag("Minion");
        cam = GetComponent<Camera>();
        defaultZoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
        if (!inZoomSequence) Pivot();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Zoom", ZoomType.OnMinion);
        }
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
        Vector3 goalPos = transform.position;

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
                zoomLevel = 0;
                goalZoom = defaultZoom;
                goalPos = GetClosestMinion().transform.TransformPoint(Vector3.zero);
                break;
        }

        //Animation

        bool reachedGoalZoom = Mathf.Abs(cam.orthographicSize - goalZoom) < 0.5f;
        bool reachedGoalPos = (transform.position - goalPos).magnitude < 0.1f;

        while (!reachedGoalZoom || !reachedGoalPos)
        {
            transform.position += (goalPos - transform.position) / (8 / zoomSpeed);
            cam.orthographicSize += (goalZoom - cam.orthographicSize) / (8 / zoomSpeed);
            yield return new WaitForSeconds(0.01f);
            if (!reachedGoalZoom) reachedGoalZoom = Mathf.Abs(cam.orthographicSize - goalZoom) < 0.5f;
            if (!reachedGoalPos) reachedGoalPos = (transform.position - goalPos).magnitude < 0.1f;
        }

        inZoomSequence = false;
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
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return minions[closestIndex];
    }
}
