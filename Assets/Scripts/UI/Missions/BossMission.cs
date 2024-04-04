using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMission : MonoBehaviour
{
    public Objectives objective;
    public AshPC player;
    public GameObject missionUIPopup;
    public GameObject continueText;

    public Text messageText;
    private float timer;
    private float timerSet = 4;
    private bool textHasStarted;
    
    private bool hasInteracted;
    
    private TextWriter.TextWriterSingle textWriterSingle;
    private bool messageCompleted;

    public GameObject flyByCam;
    private bool hasFlown;
    private bool text1;



    // Start is called before the first frame update
    void Start()
    {
        timer = timerSet;
        //messageText.text = "Hello World!";
        continueText.SetActive(false);
        missionUIPopup.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (textHasStarted)
        {
            if (Input.anyKeyDown && messageCompleted)
            {
                timer = 0;
                messageCompleted = false;
            }
            if (Input.anyKeyDown && textWriterSingle != null && textWriterSingle.IsActive())
            {
                textWriterSingle.WriteAllAndDestroy();
                messageCompleted = true;
            }
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if(!hasFlown)
                {
                    missionUIPopup.SetActive(false);
                    flyByCam.SetActive(true);
                    timer = timerSet;
                    hasFlown = true;

                }
                else
                {
                    flyByCam.SetActive(false);

                    if (!text1)
                    {
                        missionUIPopup.SetActive(true);
                        textWriterSingle = TextWriter.AddWriter_Static(messageText, "Please help us out!", .025f, true, true);
                        timer = 1;
                        text1 = true;
                    }
                    else
                    {

                        continueText.SetActive(true);
                        if (Input.anyKeyDown)
                        {
                        
                            timer = 4;
                            textHasStarted = false;
                            continueText.SetActive(false);
                            missionUIPopup.SetActive(false);
                            player.SetCanMove(true);
                            //objective.enabled = true;
                            objective.GetComponent<Objectives>().UpdateObjText("Defeat Tempest!");
                            player.isBossLevel = true;
                        }
                    }
                    
                }

                


            }


        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if (!textHasStarted && !hasInteracted)
        {

            if (other.tag == "Player")
            {
                hasInteracted = true;
                missionUIPopup.SetActive(true);
                player.SetCanMove(false);
                player.anim.SetFloat("MoveForward", 0);
                player.anim.SetFloat("MoveRight", 0);
                textWriterSingle = TextWriter.AddWriter_Static(messageText, "Hey Mister! Some lady came in like a Tempest and started shooting clouds at the town. The clouds are chasing the villagers around!", .025f, true, true);
                textHasStarted = true;
                
                timer = 4;

            }
        }
        else if(!textHasStarted && hasInteracted)
        {

        }
    }
}

