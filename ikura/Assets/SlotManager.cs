using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 

public class SlotManager : MonoBehaviour
{
    public int Figure_num = 5;
    public float RotatePerSecond = 1f;
    public float CylinderRasius = 13;

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
    public int[] OutFigureIndex; // 外れる時の画像３つを選ぶための配列
    public bool AllCylindersHasStopped;
    
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        CylinderObj = new GameObject[3];
        StoppedFigure = new GameObject[3];
        CC = new CylinderController[3];
        OutFigureIndex = new int[Figure_num];
        for (int i = 0; i < Figure_num; i++)
        {
            OutFigureIndex[i] = i;
        }
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
        WinPerform();
        if(CheckAllCylindersHasStopped() == 3) AllCylindersHasStopped = true;
        CloseCylindersStatus = CloseCylindersHasStopped();
        if(InteractSlot && ! InteractSlot_prev){
            Win = false;
            CloseWin = false;
            AllCylindersHasStopped = false;
            CheckWin = Random.Range(0, 5);
            OutCylinderIndex = Random.Range(0, 3); // 1個だけ外れるシリンダーのインデックス番号を決める
            StopFigureIndex = Random.Range(0, Figure_num); //止まる画像のインデックス番号を決める
            Shuffle(OutFigureIndex);
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

    public void WinPerform(){
        if(AllCylindersHasStopped && Win){
            for (int i = 0; i < 3; i++)
            {
                CC[i].FigureObj[StopFigureIndex].GetComponent<FigureController>().Win = true;
            }
        }
    }

    public void EndPerform(){
        Win = false;
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

    public int CheckAllCylindersHasStopped(){  //3つのシリンダーが止まっているか
        int SumBools = 0;
        for (int i = 0; i < 3; i++)
        {
            if(CC[i].hasStopped){
                SumBools++;
            }
        }
        return SumBools;
    }

    void Shuffle(int[] num) 
    {
        for (int i = 0; i < num.Length; i++)
        {
　　　　　　 //（説明１）現在の要素を預けておく
            int temp = num[i]; 
　　　　　　 //（説明２）入れ替える先をランダムに選ぶ
            int randomIndex = Random.Range(0, num.Length); 
　　　　　　 //（説明３）現在の要素に上書き
            num[i] = num[randomIndex]; 
　　　　　　 //（説明４）入れ替え元に預けておいた要素を与える
            num[randomIndex] = temp; 
        }
    }
}
