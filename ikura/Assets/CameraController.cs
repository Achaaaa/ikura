using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject TargetObj;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = TargetObj.transform.position + offset;
    }
}
