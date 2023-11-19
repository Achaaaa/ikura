using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotReposition : MonoBehaviour
{
    public GameObject SlotManagerObj;
    public Camera CamObj;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 screen_point = Input.mousePosition;
        Vector3 screen_point = this.GetComponent<RectTransform>().anchoredPosition;
        screen_point.z = 43.0f;
        cube.transform.position = CamObj.ScreenToWorldPoint(screen_point);
        SlotManagerObj.transform.position = CamObj.ScreenToWorldPoint(screen_point);
        //SlotManagerObj.transform.position = CamObj.ScreenToWorldPoint(screen_point);
    }
}
