using System.Collections.Generic;
using UnityEngine;

public class PathTrail : MonoBehaviour
{
    public List<Transform> checkpoints;
    public void DrawLitTrailToCheckpoint()
    {
        LineRenderer lineRenderer = new LineRenderer();
        lineRenderer.positionCount = checkpoints.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        for (int i = 0; i < checkpoints.Count; i++)
        {
            lineRenderer.SetPosition(i, checkpoints[i].transform.position);
        }
    }
}
