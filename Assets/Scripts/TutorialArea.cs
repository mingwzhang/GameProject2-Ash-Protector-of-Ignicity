using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialArea : MonoBehaviour
{
    public AshPC player;
    public CaveFlyover FlyoverCamera;
    public GameObject missionUIPopup;
    public GameObject continueText;
    
    public Text messageText;
    private float timer = 8;
    private bool textHasStarted;
    private bool nextText1;
    private bool nextText2;
    private bool nextText3;
    private bool nextText4;
    private bool hasInteracted;
    private bool messageCompleted;

    private TextWriter.TextWriterSingle textWriterSingle;



    // Start is called before the first frame update
    void Start()
    {
        continueText.SetActive(false);
        missionUIPopup.SetActive(false);
        FlyoverCamera.gameObject.SetActive(false);
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
                    timer = 8;
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "I'd go investigate, but there's skeletons in the way and I can't double jump like you. Use your double jump to get through the cave and don't touch the water, it'll extinguish your flame." , .025f, true, true);

                }
                else if (!nextText2)
                {
                    nextText2 = true;
                    timer = 8;
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "This tunnel leads to some sort of puzzle, but I'm not sure how to solve it. There's a few totems scattered around, maybe those do something?" , .025f, true, true);
                    FlyoverCamera.Flyover();

                }
                else if (!nextText3)
                {
                    nextText3 = true;
                    timer = 8;
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "Mages seem to be guarding these totems and dodging their attacks would be pretty difficult without some kind of dash. It's great that you have one though, dashing can get you of some sticky situations!" , .025f, true, true);
                    
                }
                else if (!nextText4)
                {
                    nextText4 = true;
                    timer = 4;
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "Oh! Last thing, there's a few torches around the cave. Remember to use them when your health gets low!" , .025f, true, true);
                    
                }
                else
                {
                    

                    continueText.SetActive(true);

                    if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                    {
                        textHasStarted = false;
                        continueText.SetActive(false);
                        missionUIPopup.SetActive(false);
                        player.SetCanMove(true);
                        FlyoverCamera.MainCamToggle();
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
                textWriterSingle = TextWriter.AddWriter_Static(messageText, "Hey Ash, good to see you! Ever since the Tempest came to the village, monsters have been coming down from the mountains. The village thinks that the Tempest was using this cave as her hideout, but some eerie shadow figure has moved in recently." , .025f, true, true);
                textHasStarted = true;
                

            }
        }
    }
}