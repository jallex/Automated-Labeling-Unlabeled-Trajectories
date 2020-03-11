using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach_Person_To_Correct_Loc : MonoBehaviour
{
    public GameObject person;
    public GameObject person_block;
    public GameObject empty;

    // Start is called before the first frame update
    void Start()
    {
        //place the person at 0
        //place the empty's position and rotation to the block at 0
        //parent them 
        person.transform.position = new Vector3(0, 0, 0);
        person.transform.rotation = new Quaternion(0, 0, 0, 0);
        Vector3 block_pos = person_block.transform.position;
        Quaternion block_rot = person_block.transform.rotation;
        empty.transform.position = block_pos;
        empty.transform.rotation = block_rot;
        person.transform.SetParent(empty.transform);

        // set transform and rotation of empty 
        //correct rotation
        empty.transform.position = new Vector3(0, 0, 0);
        empty.transform.rotation = new Quaternion(0, 0, 0, 0);
        empty.transform.Rotate(-90, 180, 0);
    }
}
