using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustPlane : MonoBehaviour
{
    public Camera[] cameras;
    // Start is called before the first frame update
    void Start()
    {
        // get camera objects in scene
        cameras = GameObject.FindObjectsOfType<Camera>();
        // Debug.Log("Number of Cameras: " + cameras.Length);
        for (int i = 0; i < cameras.Length; i++)
        {
            //set the near and far clipping plane values 
            cameras[i].nearClipPlane = .03f;
            cameras[i].farClipPlane = 10;

            //get child of camera
            if (cameras[i].transform.GetChild(0).gameObject)
            {
                GameObject mesh = cameras[i].transform.GetChild(0).gameObject;
                ObjectRotation(mesh);
                cameras[i].Render();
            }
            //Optional:
            //add the models as children 
            //rotate all micus cameras Y by 180
            //scale ocus to 0.02, also rotate Y by 180
        }

        }
    public void ObjectRotation(GameObject you)
    {
        you.transform.localEulerAngles = new Vector3(0, 180, 0);
    }
}

