using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public int Credit;
    SlotManager SM;
    // Start is called before the first frame update
    void Start()
    {
        SM = this.transform.root.gameObject.GetComponent<SlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConsumeCredit(){
        Credit--;
    }
}
