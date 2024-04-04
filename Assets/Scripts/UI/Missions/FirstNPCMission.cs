using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstNPCMission : MonoBehaviour
{
    public Objectives objective;
    public AshPC player;
    public GameObject missionUIPopup;
    public GameObject continueText;
    
    public Text messageText;
    private float timer = 6;
    private bool textHasStarted;
    private bool nextText1;
    private bool nextText2;
    private bool hasInteracted;
    private bool messageCompleted;

    private TextWriter.TextWriterSingle textWriterSingle;



    // Start is called before the first frame update
    void Start()
    {

        //messageText.text = "Hello World!";
        continueText.SetActive(false);
        missionUIPopup.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(textHasStarted)
        {
            if(Input.anyKeyDown && messageCompleted)
            {
                timer = 0;
                messageCompleted = false;
            }
            if(Input.anyKeyDown && textWriterSingle != null && textWriterSingle.IsActive())
            {
                textWriterSingle.WriteAllAndDestroy();
                messageCompleted = true;
            }
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if(!nextText1)
                {
                    nextText1 = true;
                    timer = 4;
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "You can move around with W,A,S,D or Left Joystick to get where you need to go!", .025f, true, true);

                }
                else
                {
                    if(!nextText2)
                    {
                        nextText2 = true;
                        timer = 4;
                        textWriterSingle = TextWriter.AddWriter_Static(messageText, "You can also look around with the Mouse or Right Joystick and jump with the Space bar or the A/X button. \n\n\n Good Luck ASH!!!", .025f, true, true);
                    }
                    else
                    {

                        continueText.SetActive(true);
            
                        if(Input.anyKeyDown)
                        {
                            textHasStarted = false;
                            continueText.SetActive(false);
                            missionUIPopup.SetActive(false);
                            player.SetCanMove(true);
                            //objective.enabled = true;
                            objective.GetComponent<Objectives>().UpdateObjText("Cross the bridge to go relight the Sacred Totems!");
                        }
                    }
                }
            
            
            }


        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if(!textHasStarted && !hasInteracted)
        {
            
            if(other.tag == "Player")
            {
                
                hasInteracted = true;
                missionUIPopup.SetActive(true);
                player.SetCanMove(false);
                player.anim.SetFloat("MoveForward", 0);
                player.anim.SetFloat("MoveRight", 0);
                textWriterSingle = TextWriter.AddWriter_Static(messageText, "Hey Ash!.... There's a huge storm coming in, it looks dangerous. All the Sacred Totems north of town went out. Can you please hurry across the bridge and relight them before the storm gets here?!", .025f, true, true);
                textHasStarted = true;

            }
        }
    }
}
