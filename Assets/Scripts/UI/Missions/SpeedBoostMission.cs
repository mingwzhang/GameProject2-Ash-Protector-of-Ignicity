using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoostMission : MonoBehaviour
{
    public Objectives objective;
    public AshPC player;
    public GameObject missionUIPopup;
    public GameObject continueText;
    
    public Text messageText;
    private float timer = 6f;
    private float timer2 = .75f;
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
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {

                player.SetCanMove(false);
                player.anim.SetFloat("MoveForward", 0);
                player.anim.SetFloat("MoveRight", 0);



                timer -= Time.deltaTime;
                if (timer <= 0)
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
                textWriterSingle = TextWriter.AddWriter_Static(messageText, "Hey Ash, that winged shoe over there will grant you boosted speed temporarily.\nYou should give it a shot to get through this thick fog on top of the mountain!", .025f, true, true);
                textHasStarted = true;

            }
        }
    }
}

