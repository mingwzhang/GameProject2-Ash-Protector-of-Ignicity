using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMission2 : MonoBehaviour
{
    public Objectives objective;
    public AshPC player;
    public GameObject missionUIPopup;
    public GameObject continueText;
    
    public Text messageText;
    private float timer = 6;
    private bool textHasStarted;
    private bool nextText1;
    private bool hasInteracted;
    public GameObject treeCol;
    private TextWriter.TextWriterSingle textWriterSingle;
    private bool messageCompleted;



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
                if (!nextText1)
                {
                    nextText1 = true;
                    timer = 3;
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "You can throw a fireball by pressing the Left Mouse Button, or by pressing the RB/R1 Button!", .025f, true, true);

                }
                else
                {
                    

                    continueText.SetActive(true);

                    if (Input.anyKeyDown)
                    {
                        treeCol.SetActive(false);
                        nextText1 = false;
                        timer = 4;
                        textHasStarted = false;
                        continueText.SetActive(false);
                        missionUIPopup.SetActive(false);
                        player.SetCanMove(true);
                        //objective.enabled = true;
                        objective.GetComponent<Objectives>().UpdateObjText("Destroy the tree with a fireball!");
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
                textWriterSingle = TextWriter.AddWriter_Static(messageText, "Hey Ash! There's a tree blocking the bridge. Could you through a fireball to burn it away?!", .025f, true, true);
                textHasStarted = true;
                nextText1 = false;
                timer = 4;

            }
        }
    }
}
