
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

//script that moves the cube to the position and rotation of near clipping plane of the given camera, 
// and adjusts its size to fit the near clipping plane through parenting
public class MoveToNearPlane : MonoBehaviour
{
    public RawImage rawImage;
    public Camera cam;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        //the position of the camera
        Vector3 pos = cam.transform.position;
        //the rotation of the camera
        Quaternion r = cam.transform.rotation;
        //set camera's position to origin and make rotation 0.
        cam.transform.rotation = new Quaternion(0, 0, 0, 0);
        cam.transform.position = new Vector3(0, 0, 0);
        //transform the cube's rotation and position to be that of the near clipping plane
        cube.transform.localRotation = cam.transform.localRotation;
        cube.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.position.y, cam.transform.position.z + cam.nearClipPlane);
       
        //calculate the width and height of the near clipping plane based on FOV
        float h = 2.0f * 0.3f * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float w = h * cam.aspect;

        //set scale of cube
        cube.transform.localScale = new Vector3(w, h, 0.02f);
        //set parent of cube to be the camera
        cube.transform.SetParent(cam.transform);
        //transform the camera back to its original position and rotation
        cam.transform.position = pos;
        cam.transform.rotation = r;
        //rotate cam 90 degrees on the z axis
        //cam.transform.localRotation = new Quaternion(cam.transform.localRotation.x, cam.transform.localRotation.y, 0, cam.transform.localRotation.w);
    }
}
