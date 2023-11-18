using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 

public class SlotManager : MonoBehaviour
{
    public bool InteractSlot;
    public PlayableDirector playableDirector;
    public bool IsCorrect;
    GameObject[] CylinderObj;
    CylinderController[] CC;
    public GameObject[] StoppedFigure;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        CylinderObj = new GameObject[3];
        StoppedFigure = new GameObject[3];
        CC = new CylinderController[3];
        for (int i = 0; i < 3; i++)
        {
            Transform childTransform = this.transform.GetChild(i);
            CylinderObj[i] = childTransform.gameObject;
            CC[i] = CylinderObj[i].GetComponent<CylinderController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(InteractSlot) {
            playableDirector.Play();
            IsCorrect = false;
        }
        else StopTimeline();
        for (int i = 0; i < 3; i++)
        {
            StoppedFigure[i] = CC[i].StoppedFigure;
        }
        if(StoppedFigure[0].name == StoppedFigure[1].name && StoppedFigure[1].name == StoppedFigure[2].name){
            IsCorrect = true;
        }

    }

    public void StopTimeline(){
        InteractSlot = false;
        playableDirector.Stop();
    }
}
