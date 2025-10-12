using System.Collections.Generic;
using UnityEngine;

public class PathTrail : MonoBehaviour
{
    [SerializeField] private List<Transform> checkpoints = new List<Transform>();
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject redLinePrefab;
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

        DrawLitTrailToCheckpoints(10f, linePrefab);
    }

    public void DrawRedLitTrailToCheckpoint()
    {
        GameObject line = Instantiate(redLinePrefab);
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = checkpoints.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        for (int i = 0; i < checkpoints.Count; i++)
        {
            lineRenderer.SetPosition(i, checkpoints[i].transform.position);
        }

        DrawLitTrailToCheckpoints(10f, redLinePrefab);
    }

    public void DrawLitTrailToCheckpoints(float yOffset, GameObject prefab)
    {
        GameObject line = Instantiate(prefab);
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = checkpoints.Count;
        lineRenderer.startWidth = 0.6f;
        lineRenderer.endWidth = 2f;
        for (int i = 0; i < checkpoints.Count; i++)
        {
            Vector3 offsetPosition = checkpoints[i].transform.position;
            offsetPosition.y = offsetPosition.y + yOffset;
            lineRenderer.SetPosition(i, offsetPosition);
        }
    }
}
