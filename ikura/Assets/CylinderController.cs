using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; //Timelineの制御に必要

public class CylinderController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public AnimationCurve animationCurve;

    public int Figure_num = 5;
    public GameObject FigurePref;
    private GameObject[] FigureObj;
    Vector3[] Figure_pos_origin;
    public Material[] FigureMaterial;
    public float CylinderRasius = 10;
    public float RotatePerSecond = 1f;
    public float CountTime = 0; //(s)
    public bool isStopping = false;
    public bool hasStopped = false;
    public float timeStamp;
    bool isStopping_prev;
    public GameObject StoppedFigure;
    public Vector3 RootOriginPos;
    float FigureRandomOffset;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        RootOriginPos = this.transform.position;
        FigureRandomOffset = (2 * Mathf.PI / Figure_num ) * Random.Range(1, Figure_num);
        InstantiateFigures();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStopping_prev && !isStopping) {
            hasStopped = false;
        }
        
        if(!isStopping_prev && isStopping) timeStamp = CountTime;
        if(isStopping) {
            playableDirector.Play();
            //if((CountTime - timeStamp) < 3f) RotatePerSecond -= Time.deltaTime / 6f;
            Debug.Log("hi");
        }
        if(isStopping && JustAngle()) hasStopped = true;
        if(!hasStopped) RotateCylinder();
        //Debug.Log(JustAngle());
        //Debug.Log(CountTime % (1f /Figure_num));
        isStopping_prev = isStopping;
    }

    void InstantiateFigures(){
        FigureObj  = new GameObject[Figure_num];
        Figure_pos_origin = new Vector3[Figure_num];
        for (int i = 0; i < Figure_num; i++)
        {
            Figure_pos_origin[i].x = RootOriginPos.x;
            Figure_pos_origin[i].z = RootOriginPos.z + CylinderRasius * Mathf.Cos(2 * Mathf.PI / Figure_num * i + FigureRandomOffset);
            Figure_pos_origin[i].y = RootOriginPos.y + CylinderRasius * Mathf.Sin(2 * Mathf.PI / Figure_num * i + FigureRandomOffset);
            Instantiate(FigurePref, Figure_pos_origin[i], Quaternion.identity, this.transform);
        }
        
        for (int i = 0; i < Figure_num; i++)
        {
            Transform childTransform = this.transform.GetChild(i);
            FigureObj[i] = childTransform.gameObject;
            FigureObj[i].name = "figure_" + i.ToString();
            FigureObj[i].GetComponent<MeshRenderer>().material = FigureMaterial[i];
            FigureObj[i].transform.rotation = Quaternion.AngleAxis(90f, new Vector3(1f,0f,0f)) * this.transform.rotation; 
        }
    }

    bool JustAngle(){
        for (int i = 0; i < Figure_num; i++)
        {
            if(-0.1f < FigureObj[i].transform.position.y && FigureObj[i].transform.position.y < 0.1f && 0f < FigureObj[i].transform.position.z) {
                StoppedFigure = FigureObj[i];
                return true;
            }
        }
        return false;
    }

    void RotateCylinder(){
        CountTime += Time.deltaTime; 
        for (int i = 0; i < Figure_num; i++)
        {
            FigureObj[i].transform.position = new Vector3(RootOriginPos.x, CylinderRasius * Mathf.Sin(2 * Mathf.PI * RotatePerSecond * CountTime + (2 * Mathf.PI / Figure_num * i) + FigureRandomOffset), CylinderRasius * Mathf.Cos(2 * Mathf.PI * RotatePerSecond * CountTime + (2 * Mathf.PI / Figure_num * i) + FigureRandomOffset));
        }
    }

    public void InteractCylinder(){
        if(isStopping) isStopping = false;
        else Invoke("StopCylinder", Random.Range(0.5f, 2.0f));
    }

    public void StopCylinder(){
        isStopping = true;
    }
}
