using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; //Timelineの制御に必要

public class CylinderController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public AnimationCurve animationCurve;
    public GameObject FigurePref;
    public GameObject[] FigureObj;
    Vector3[] Figure_pos_origin;
    public Material[] FigureMaterial;
    public float CountTime = 0; //(s)
    public bool isStopping = false;
    public bool hasStopped = false;
    public float timeStamp;
    bool isStopping_prev;
    public GameObject StopFigure;
    public Vector3 RootOriginPos;
    float FigureRandomOffset;
    public bool Win;
    SlotManager SM;
    public int MyIndex;
    public int[] ShuffledFigureIndex;

    void Start()
    {
        playableDirector = this.GetComponent<PlayableDirector>();
        SM = this.transform.root.gameObject.GetComponent<SlotManager>();
        RootOriginPos = this.transform.position;
        FigureRandomOffset = (2 * Mathf.PI / SM.Figure_num ) * Random.Range(1, SM.Figure_num);
        InstantiateFigures();
    }

    // Update is called once per frame
    void Update()
    {
        MyIndex = CheckCylinderIndex();
        if(isStopping_prev && !isStopping) {
            hasStopped = false;
        }
        if(!isStopping_prev && isStopping) timeStamp = CountTime;
        if(isStopping) {
            playableDirector.Play();
        }
        if(isStopping && JustAngle()) hasStopped = true;
        if(!hasStopped) RotateCylinder();
        isStopping_prev = isStopping;
    }

    void InstantiateFigures(){
        FigureObj  = new GameObject[SM.Figure_num];
        Figure_pos_origin = new Vector3[SM.Figure_num];
        ShuffledFigureIndex = new int[SM.Figure_num];
        for (int i = 0; i < SM.Figure_num; i++)
        {
            ShuffledFigureIndex[i] = i;
            Figure_pos_origin[i].x = RootOriginPos.x;
            Figure_pos_origin[i].z = RootOriginPos.z + SM.CylinderRasius * Mathf.Cos(2 * Mathf.PI / SM.Figure_num * i + FigureRandomOffset);
            Figure_pos_origin[i].y = RootOriginPos.y + SM.CylinderRasius * Mathf.Sin(2 * Mathf.PI / SM.Figure_num * i + FigureRandomOffset);
        }

        Shuffle(ShuffledFigureIndex);
        
        for (int i = 0; i < SM.Figure_num; i++)
        {
            Instantiate(FigurePref, Figure_pos_origin[ShuffledFigureIndex[i]], Quaternion.identity, this.transform);
            Transform childTransform = this.transform.GetChild(i);
            FigureObj[i] = childTransform.gameObject;
            FigureObj[i].name = "figure_" + i.ToString();
            FigureObj[i].GetComponent<MeshRenderer>().material = FigureMaterial[i];
            FigureObj[i].transform.rotation = Quaternion.AngleAxis(90f, new Vector3(1f,0f,0f)) * this.transform.rotation; 
        }
    }

    bool JustAngle(){
        for (int i = 0; i < SM.Figure_num; i++)
        {
            if(-0.5f < FigureObj[i].transform.position.y && FigureObj[i].transform.position.y < 0.5f && 0f < FigureObj[i].transform.position.z) {
                if(SM.Win){
                    if(i == SM.StopFigureIndex)return true;
                }else if(SM.CloseWin){                              //惜しい場合
                    if(SM.OutCylinderIndex == CheckCylinderIndex()){    //外れるシリンダーの場合
                        if(i != SM.StopFigureIndex && SM.CloseCylindersStatus == 2) return true;         //指定した画像じゃない時に止まる //他二つのシリンダーが止まっている時
                    }else{                                              //２つまで揃うシリンダーの場合
                        if(i == SM.StopFigureIndex)return true;         //指定した画像で止まる
                    }
                }else{                                              //完全にハズレの場合
                    if(i == SM.OutFigureIndex[MyIndex])return true;         
                }
            }
        }
        return false;
    }

    int CheckCylinderIndex(){
        return int.Parse(this.gameObject.name.Replace("Cylinder_", ""));
    }

    void RotateCylinder(){
        CountTime += Time.deltaTime; 
        for (int i = 0; i < SM.Figure_num; i++)
        {
            FigureObj[i].transform.position = new Vector3(RootOriginPos.x, SM.CylinderRasius * Mathf.Sin(2 * Mathf.PI * SM.RotatePerSecond * CountTime + (2 * Mathf.PI / SM.Figure_num * ShuffledFigureIndex[i]) + FigureRandomOffset), SM.CylinderRasius * Mathf.Cos(2 * Mathf.PI * SM.RotatePerSecond * CountTime + (2 * Mathf.PI / SM.Figure_num * ShuffledFigureIndex[i]) + FigureRandomOffset));
        }
    }

    public void InteractCylinder(){
        if(isStopping) isStopping = false;
        else Invoke("StopCylinder", Random.Range(0.0f, 0.0f));
    }

    public void StopCylinder(){
        isStopping = true;
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
