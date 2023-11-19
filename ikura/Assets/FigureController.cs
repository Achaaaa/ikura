using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; //Timelineの制御に必要

public class FigureController : MonoBehaviour
{
    public Animator anim;
    private PlayableDirector playableDirector;
    public bool IsWinPerforming;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsWinPerforming) {
            playableDirector.Play();
        }
    }

    public void EndPerform(){
        IsWinPerforming = false;
        playableDirector.Stop();
    }
}
