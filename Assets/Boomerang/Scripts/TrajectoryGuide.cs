using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrajectoryGuide : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float angle;
    [SerializeField] float centerPoint = 5f;
    private Vector3 offset;

    private LineRenderer lineRenderer;

    private void Start()
    {
        Player = PlayerManager.instance.player.transform;
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing.");
            return;
        }

        PlayerMovement.onPlayerMove.AddListener(DrawTrajectory);
        PlayerActions.onPlayerShoot.AddListener(GuideSwitch);

    }
    private void Update()
    {
        Vector3 pointedEndPosition = CalculatePosition(0);
        offset = new Vector3(0, (Player.position.y + 3) - pointedEndPosition.y, Player.position.z - pointedEndPosition.z);

        centerPoint = PlayerManager.instance.force.value;
    }

    public void ChangeTrajectory()
    {
        centerPoint = PlayerManager.instance.force.value;
        DrawTrajectory();
    }
    Vector3 CalculatePosition(float angle)
    {
        float x = (1 - Mathf.Cos(angle)) * Mathf.Sin(angle) * centerPoint;
        float y = transform.position.y;
        float z = -Mathf.Cos(angle) * centerPoint;

        return new Vector3(x, y, z);
    }

    void DrawTrajectory()
    {
        if(this.GetComponent<LineRenderer>().enabled == false)
        {
            this.GetComponent<LineRenderer>().enabled = true;
        }
        int segments = 100; 
        lineRenderer.positionCount = segments + 1; 
        Vector3[] positions = new Vector3[segments + 1];

        float playerYRotation = Player.eulerAngles.y; 
        Quaternion rotation = Quaternion.Euler(0, playerYRotation, 0); 

        for (int i = 0; i <= segments; i++)
        {
            float segmentAngle = (i * 2 * Mathf.PI) / segments;
            Vector3 position = CalculatePosition(segmentAngle) + offset;
            positions[i] = rotation * position; 
        }

        lineRenderer.SetPositions(positions);
        CheckTarget();
    }
    void CheckTarget()
    {
        float tolerance;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pathPoint = lineRenderer.GetPosition(i);
            // Compare X and Z axis only
            pathPoint = new Vector3 (pathPoint.x, 0, pathPoint.z);
            GameObject[] targets;
            targets = GameObject.FindGameObjectsWithTag("Target");
            foreach (var target in targets)
            {
                Vector3 targetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);
                float distance = Vector3.Distance(pathPoint, targetPos);
                Outline outline;
                outline = target.transform.GetComponent<Outline>();
                tolerance = target.transform.localScale.z / 2;
                if (distance <= tolerance)
                { 
                    if (outline != null)
                    {
                        outline.OutlineWidth = 10;
                    }

                    Debug.Log("Target is within the path!");
                    return;
                }
                else
                {
                    if (outline != null)
                    {
                        outline.OutlineWidth = 0;
                    }
                }
            }
        }

        Debug.Log("Target is not within the path.");
    }
    public void GuideSwitch()
    {
        if(this.GetComponent<LineRenderer>().enabled == true)
        {
            this.GetComponent<LineRenderer>().enabled = false;
        }
        else
            this.GetComponent<LineRenderer>().enabled = true;
    }
}
