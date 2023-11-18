using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 

public class SlotManager : MonoBehaviour
{
    public int Figure_num = 5;
    public float RotatePerSecond = 1f;
    public bool InteractSlot;
    public PlayableDirector playableDirector;
    public bool Win;
    public bool CloseWin;
    GameObject[] CylinderObj;
    CylinderController[] CC;
    public GameObject[] StoppedFigure;
    public int CheckWin;
    bool InteractSlot_prev;
    public int StopFigureIndex; 
    public int OutCylinderIndex;
    public int CloseCylindersStatus;
    
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

        CloseCylindersStatus = CloseCylindersHasStopped();

        if(InteractSlot && ! InteractSlot_prev){
            Win = false;
            CloseWin = false;
            CheckWin = Random.Range(0, 2);
            OutCylinderIndex = Random.Range(0, 3); // 1個だけ外れるシリンダーのインデックス番号を決める
            StopFigureIndex = Random.Range(0, Figure_num); //止まる画像のインデックス番号を決める
        }
        if(CheckWin == 0)Win = true;
        if(0 < CheckWin && CheckWin < 2){
            CloseWin = true;
        }
        if(InteractSlot) {
            playableDirector.Play();
        }
        else StopTimeline();
        // if(StoppedFigure[0].name == StoppedFigure[1].name && StoppedFigure[1].name == StoppedFigure[2].name){
        //     Win = true;
        // }
        InteractSlot_prev = InteractSlot;
    }

    public void StopTimeline(){
        InteractSlot = false;
        playableDirector.Stop();
    }

    public int CloseCylindersHasStopped(){  //惜しい２つのシリンダーが止まっているか
        int SumBools = 0;
        for (int i = 0; i < 3; i++)
        {
            if(i != OutCylinderIndex && CC[i].hasStopped){
                SumBools++;
            }
        }
        return SumBools;
    }
}
