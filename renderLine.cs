using UnityEngine;

public class renderLine : MonoBehaviour
{
    public Camera cam;
    public GameObject endPoint;

    void LateUpdate()
    {
            LineRenderer lr = cam.GetComponent<LineRenderer>();
            // Set some positions
            Vector3[] positions = new Vector3[2];
            positions[0] = cam.transform.position;
            positions[1] = endPoint.transform.position;
            lr.positionCount = positions.Length;
            lr.SetPositions(positions);
    }
}
