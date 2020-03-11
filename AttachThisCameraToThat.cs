using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachThisCameraToThat : MonoBehaviour
{
    //cam that will be the parent
    public Camera cam1;
    //the fbx Object that will be the child 
    public GameObject empty;
    public Camera fbxCam;
    public GameObject fbx;
    // Start is called before the first frame update
    void Start()
    {
        empty.transform.position = fbxCam.transform.position;
        empty.transform.rotation = fbxCam.transform.rotation;

        fbx.transform.SetParent(empty.transform);
        //the position of the first camera
        Vector3 pos = cam1.transform.position;
        //the rotation of the camera
        Quaternion r = cam1.transform.rotation;
        cam1.transform.rotation = empty.transform.rotation;
        cam1.transform.position = empty.transform.position;
        empty.transform.SetParent(cam1.transform);
        cam1.transform.rotation = r;
        cam1.transform.position = pos;
        //empty.transform.Rotate(0, 0, 90);
    }
}
