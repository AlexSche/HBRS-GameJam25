using System.Collections.Generic;
using UnityEngine;

public class PathTrail : MonoBehaviour
{
    [SerializeField] private List<Transform> checkpoints = new List<Transform>();
    [SerializeField] private GameObject linePrefab;
    public void DrawLitTrailToCheckpoint()
    {
        GameObject line = Instantiate(linePrefab);
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = checkpoints.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        for (int i = 0; i < checkpoints.Count; i++)
        {
            lineRenderer.SetPosition(i, checkpoints[i].transform.position);
        }

        DrawLitTrailToCheckpoints(4f);
    }

    public void DrawLitTrailToCheckpoints(float yOffset)
    {
        GameObject line = Instantiate(linePrefab);
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = checkpoints.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        for (int i = 0; i < checkpoints.Count; i++)
        {
            Vector3 offsetPosition = checkpoints[i].transform.position;
            offsetPosition.y = offsetPosition.y + yOffset;
            lineRenderer.SetPosition(i, offsetPosition);
        }
    }
}
