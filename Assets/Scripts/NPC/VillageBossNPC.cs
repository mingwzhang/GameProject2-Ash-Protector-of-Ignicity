using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VillageBossNPC : MonoBehaviour
{
    private NavMeshAgent npc;
    private GameObject player;
    public Animator playerAnim;
    private bool isTraveling;
    private Animator anim;
    public Objectives objective;
    public GameObject missionUIPopup;
    public GameObject continueText;
    public Text messageText;
    private float timer = 6;
    private bool textHasStarted;
    private bool nextText1;
    private bool hasInteracted;
    private TextWriter.TextWriterSingle textWriterSingle;
    private bool messageCompleted;
    private bool hasYelled;
    public GameObject bridgeCol;

    // Start is called before the first frame update
    void Start()
    {
        npc = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Character");
        isTraveling = true;
        continueText.SetActive(false);
        missionUIPopup.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        if (isTraveling)
        {
            npc.SetDestination(player.transform.position);
        }
        

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
                
                continueText.SetActive(true);

                if (Input.anyKeyDown)
                {
                    bridgeCol.SetActive(false);
                    nextText1 = false;
                    timer = 4;
                    textHasStarted = false;
                    continueText.SetActive(false);
                    missionUIPopup.SetActive(false);
                    player.GetComponent<AshPC>().SetCanMove(true);
                    //objective.enabled = true;
                    objective.GetComponent<Objectives>().UpdateObjText("Travel through the valley on the other side of the village and locate the mysterious Shadow!");
                }
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("hasMetAsh", true);
            isTraveling = false;
            npc.SetDestination(npc.gameObject.transform.position);
            npc.gameObject.transform.LookAt(new Vector3(player.transform.position.x, npc.gameObject.transform.position.y, player.transform.position.z));
            if (!textHasStarted && !hasInteracted)
            {

                if (other.tag == "Player")
                {
                    hasInteracted = true;
                    missionUIPopup.SetActive(true);
                    player.GetComponent<AshPC>().SetCanMove(false);
                    playerAnim.SetFloat("MoveForward", 0);
                    playerAnim.SetFloat("MoveRight", 0);
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, "Hey Ash! There's was a shadowing looking figure controlling the monsters in the village. After you defeated tempest, the monsters disappeared and the Shadow ran into the valley on the other side of the village. Could you please go see what it's up to?!", .025f, true, true);
                    textHasStarted = true;
                    nextText1 = false;
                    timer = 4;

                }
            }
        }

    }
}
