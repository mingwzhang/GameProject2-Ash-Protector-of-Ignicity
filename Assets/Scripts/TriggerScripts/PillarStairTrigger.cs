using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarStairTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator[] PillarStairList;

    private int triggerCount = 0;
    void Start()
    {
        
    }
    void Update()
    {

    }
    public void PillarStairCount()
    {
        triggerCount++;
        PillarStairTriggered();
    }

    private void PillarStairTriggered()
    {
        switch (triggerCount)
        {
            case (1):
                PillarStairList[0].SetBool("PillarTriggerEntry", true);
                break;
            case (2):
                PillarStairList[1].SetBool("PillarTrigger1", true);
                PillarStairList[2].SetBool("PillarTrigger2", true);
                break;
            case (3):
                PillarStairList[3].SetBool("PillarTrigger3", true);
                PillarStairList[4].SetBool("PillarTrigger4", true); 
                break;
            case (4):
                PillarStairList[5].SetBool("PillarTrigger5", true);
                break;
        }
    }
    // Update is called once per frame

}
