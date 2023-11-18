using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 

public class SlotManager : MonoBehaviour
{
    public bool InteractSlot;
    public PlayableDirector playableDirector;

    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(InteractSlot) playableDirector.Play();
        else StopTimeline();
    }

    public void StopTimeline(){
        InteractSlot = false;
        playableDirector.Stop();
    }
}
