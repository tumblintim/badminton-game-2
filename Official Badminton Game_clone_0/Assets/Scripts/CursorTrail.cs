using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrail : MonoBehaviour
{
    public Color trailColor = new Color(1f, 1f, 1f);
    public float distanceFromCamera = 5;
    public float startWidth = 0.1f;
    public float endWidth = 0f;
    public float trailTime = 0.24f;

    Transform trailTransform;
    Camera thisCamera;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();

        GameObject trailObj = new GameObject("Mouse Trail");
        trailTransform = trailObj.transform;
        TrailRenderer trail = trailObj.AddComponent<TrailRenderer>();
        trail.time = -1f;
        MoveTrailToCursor(Input.mousePosition);
        trail.time = trailTime;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;
        trail.numCapVertices = 2;
        trail.sharedMaterial = new Material(Shader.Find("Unlit/Color"));
        trail.sharedMaterial.color = trailColor;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject trailObj = GameObject.Find("Mouse Trail");
        TrailRenderer trail = trailObj.GetComponent<TrailRenderer>();

        if(Input.GetKey(KeyCode.Mouse0))
        {
            trail.startWidth = startWidth;
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            trail.startWidth = 0f;
        }

        MoveTrailToCursor(Input.mousePosition);
    }

    void MoveTrailToCursor(Vector3 screenPosition)
    {
        trailTransform.position = thisCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
    }
}
