using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightTorchMission : MonoBehaviour
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
                    timer = 4;
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "You can light the torches by walking up to them and pressing the Right Mouse Button, or pressing Y/Triangle.\n\nGood luck Ash!" , .025f, true, true);

                }
                else
                {
                    

                    continueText.SetActive(true);

                    if (Input.anyKeyDown)
                    {
                        textHasStarted = false;
                        continueText.SetActive(false);
                        missionUIPopup.SetActive(false);
                        player.SetCanMove(true);
                        //objective.enabled = true;
                        //objective.GetComponent<Objectives>().UpdateObjText("Light the) ;
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
                textWriterSingle = TextWriter.AddWriter_Static(messageText, "Thanks for coming Ash!\nThere are some weird monsters here with clouds following them.\nCan you light the torches in the area and clear out these monsters please!?", .025f, true, true);
                textHasStarted = true;

            }
        }
    }
}
