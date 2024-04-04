using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AshPC : MonoBehaviour
{
    public float moveSpeed = 10;
    public float moveSpeedMaster;
    private float tempMoveSpeed;
    public float gravityScale;
    public float jumpHeight = 10;
    public float health;
    private float maxHealth = 5;
    private float overhealMax = 10;
    private float timer;
    private float timerSet = 3;
    private float walkTimerSet = .35f;
    private float walkTimer;
    private float flameIntensity;
    private float startHeight;
    private float landHeight;
    private float fallDistance;
    private float startingTimer = 3;
    private float damageColorTimerSet = .1f;
    private float damageColorTimer;


    private int numOfPedestals = 10;


    private Vector3 moveDir;

    public static bool isPaused;
    public static bool hasDied = false;
    private bool useBaseMesh = false;
    private bool hasWon = false;
    private bool isBoosted;
    private bool isGivingFlame;
    private bool isStartingLevel;
    public bool canMove = true;
    private bool damagedSkin;
    private bool canJump;
    private bool canDoubleJump;


    public HealthBar healthBar;

    public GameObject pauseMenu;
    private GameObject deathText;
    public GameObject flameLight;
    public GameObject playerFlame;
    public GameObject winText;
    public Objectives objectives;
    public GameObject healthBarObj;
    public GameObject resumeButton;
    public GameObject playerCharacter;
    public GameObject playerCharMesh;
    public GameObject endLevelTrigger;
//    public GameObject stairsandwheels;
    
    

    public AudioSource walkAudio;

    public CharacterController controller;

    public Material burnedMaterial;
    private Material playerMat;
    private SkinnedMeshRenderer playerSkinMesh;
    private MeshRenderer playerMesh;
    public AudioSource ashDamageAudio;
    public AudioSource deathSizzle;
    public AudioSource pickupAudio;

    public ObjectiveTrigger totemTrigger;
    public Color skinColor;
    

    public Animator anim;
    public Animator fadeOut;

    public Vector3 checkpoint;
    public Vector3 checkpointobj;
    public bool isBossLevel;

    // Start is called before the first frame update
    void Start()
    {
        walkTimer = walkTimerSet;
        damageColorTimer = damageColorTimerSet;
        Cursor.lockState = CursorLockMode.Locked;
        numOfPedestals = GameObject.FindGameObjectsWithTag("Pedestal").Length;
        timer = timerSet;
        moveSpeedMaster = moveSpeed;
        pauseMenu = GameObject.Find("PauseMenu");
        
        hasDied = false;
        deathText = GameObject.Find("DeathText");
        deathText.SetActive(false);
        winText = GameObject.Find("WinText");
        winText.SetActive(false);
        health = 5;

        healthBar.SetMaxHealth(maxHealth);

        if(GameObject.Find("FogPostProcess"))
        {
            GameObject.Find("FogPostProcess").SetActive(false);
        }

        flameLight = GameObject.Find("CharacterFlameLight");

        if (GameObject.Find("AshMesh").GetComponent<SkinnedMeshRenderer>() != null)
        {
            playerSkinMesh = GameObject.Find("AshMesh").GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            useBaseMesh = true;
            playerMesh = GameObject.Find("Ash").GetComponent<MeshRenderer>();
        }

        playerFlame = GameObject.Find("CharacterFlame");

        pauseMenu.SetActive(false);
        healthBarObj.SetActive(false);
        isStartingLevel = true;
        canMove = false;

        skinColor = playerSkinMesh.material.color;
        checkpoint = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, playerCharacter.transform.position.z);
        

        playerMat = playerSkinMesh.material;

        if (endLevelTrigger != null)
        {
            endLevelTrigger.SetActive(false);
        }

//        if (stairsandwheels = GameObject.Find("Staircase&Wheels"))
 //       {
//            stairsandwheels = GameObject.Find("Staircase&Wheels");
//            stairsandwheels.SetActive(false);
//        }

    }


    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (isStartingLevel)
        {
            startingTimer -= Time.deltaTime;
            if(startingTimer <= 0)
            {
                isStartingLevel = false;
                healthBarObj.SetActive(true);
                canMove = true;
                if (scene.name == "LevelDesign1" && numOfPedestals > 0)
                {
                    objectives.UpdateObjText("Torches Left To Light: " + numOfPedestals.ToString() + "\nLocate The Cave Entrance!");
                }
                else if (numOfPedestals > 0)
                {
                    objectives.UpdateObjText("Torches Left To Light: " + numOfPedestals.ToString());
                }
                startingTimer = 3;

            }
        }

        if(damagedSkin && !hasDied)
        {
            damageColorTimer -= Time.deltaTime;
            if(damageColorTimer <= 0)
            {
                playerSkinMesh.material.color = skinColor;
                damagedSkin = false;
                damageColorTimer = damageColorTimerSet;
            }
        }

            flameIntensity = Random.Range(.7f * health / 10, 1.5f * health / 10);
        playerFlame.transform.localScale = new Vector3(health / 10, health / 10, health / 10);
        flameLight.GetComponent<Light>().intensity = flameIntensity;

        if (health <= 0 && !hasDied)
        {
            

            SetPlayerMaterial();
            deathSizzle.Play();
            playerFlame.SetActive(false);
            isPaused = true;
            hasDied = true;
            deathText.SetActive(true);
            anim.enabled = false;
            controller.enabled = false;

        }
        if (hasDied)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if(!isBossLevel)
                {
                    startHeight = checkpoint.y;
                    landHeight = startHeight;
                    fallDistance = 0;
                    playerCharacter.transform.position = checkpoint;
                
                
                    controller.enabled = true;
                    health = maxHealth;
                    isPaused = false;
                    deathText.SetActive(false);
                    anim.enabled = (true);
                    playerFlame.SetActive(true);
                    hasDied = false;
                    playerSkinMesh.material = playerMat;
                    playerSkinMesh.material.color = skinColor;
                    healthBar.SetHealth(health);
                    timer = 3;
                
                }
                else if(isBossLevel)
                {
                    isPaused = false;
                    hasDied = false;

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                }

            }

        }
        if (hasWon)
        {
            timer -= Time.deltaTime;
            GameObject.Find("FadeAnim").GetComponent<Animator>().SetBool("isLevelCompleted", true);

            if (timer <= 0)
            {
                isPaused = false;
                winText.SetActive(false);
                anim.enabled = (true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {

            if (totemTrigger != null)
            {

                Destroy(totemTrigger.GetComponent<ObjectiveTrigger>().tempTextObj);
                totemTrigger.ToggleTotem();
                totemTrigger = null;
            }
        }


    }

    public void Respawn()
    {
        startHeight = checkpoint.y;
        landHeight = startHeight;
        fallDistance = 0;
        playerCharacter.transform.position = checkpoint;
        controller.enabled = true;
        health = maxHealth;
        
        deathText.SetActive(false);
        anim.enabled = (true);
        playerFlame.SetActive(true);
        hasDied = false;
        playerSkinMesh.material = playerMat;
        playerSkinMesh.material.color = skinColor;
        healthBar.SetHealth(health);
        timer = 3;

    }

    public void SetHealth(float x)
    {

        if (!hasDied)
        {

            if (x <= 0 && !isGivingFlame)
            {
                ashDamageAudio.pitch = Random.Range(.95f, 1.1f);
                ashDamageAudio.Play();
                playerSkinMesh.material.color = Color.red;
                damagedSkin = true;
            }

            health += x;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            isGivingFlame = false;
            healthBar.SetHealth(health);
        }
        

    }

    public void IncreaseHealth()
    {
        maxHealth = overhealMax;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        if (!isPaused && !hasDied && canMove)
        {

            float yStore = moveDir.y;
            moveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            playerCharMesh.transform.LookAt(Vector3.Lerp(playerCharMesh.transform.forward, new Vector3(playerCharMesh.transform.position.x + moveDir.x, playerCharMesh.transform.position.y, playerCharMesh.transform.position.z + moveDir.z), 10f));
            moveDir = Vector3.ClampMagnitude(moveDir, 1) * moveSpeed;
            moveDir.y = yStore;
            anim.SetFloat("MoveForward", Input.GetAxis("Vertical"));
            anim.SetFloat("MoveRight", Input.GetAxis("Horizontal"));


            Vector3.Lerp(playerCharMesh.transform.forward, new Vector3(playerCharMesh.transform.position.x + moveDir.x, playerCharMesh.transform.position.y, playerCharMesh.transform.position.z + moveDir.z), 1f);



            if((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && controller.isGrounded)
            {
                walkTimer -= Time.deltaTime;
                if(walkTimer <= 0)
                {
                    walkAudio.Play();
                    walkTimer = walkTimerSet;
                }
            }
            else
            {
                walkTimer = walkTimerSet;
            }
                


            if (Input.GetButtonDown("Jump"))
            {
                
                if (!canDoubleJump && canJump)
                {
                    anim.SetBool("isAshJumping", true);
                    GetComponent<AudioSource>().pitch = Random.Range(.85f, 1.15f);
                    GetComponent<AudioSource>().Play();
                    moveDir.y = 0;
                    moveDir.y = jumpHeight;
                    canDoubleJump = true;
                    canJump = false;
                    landHeight = transform.position.y;
                }
                else if(!controller.isGrounded && canDoubleJump)
                {
                    anim.SetBool("doubleJumping", true);
                    landHeight = transform.position.y;
                    GetComponent<AudioSource>().pitch = Random.Range(.85f, 1.15f);
                    GetComponent<AudioSource>().Play();
                    moveDir.y = jumpHeight;
                    canDoubleJump = false;
                }
                
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                if(controller.isGrounded)
                {
                    //canMove = false;
                    anim.SetTrigger("Punch");
                    //anim.SetBool("isPunching", true);

                }
            }
            else if (controller.isGrounded)
            {
                canJump = true;
                landHeight = transform.position.y;
                if (anim.GetBool("isAshJumping") == true)
                {
                    anim.SetBool("isAshJumping", false);
                    string heightFell;

                    fallDistance = startHeight - landHeight;
                    heightFell = fallDistance.ToString();
                    
                    if (fallDistance > 10)
                    {


                        SetHealth(-fallDistance / 5);

                    }


                }
                if(canDoubleJump == true)
                {
                    canDoubleJump = false;
                }

                if(anim.GetBool("doubleJumping") == true)
                {
                    anim.SetBool("doubleJumping", false);
                }

                moveDir.y = -5;

            }
            else
            {
                anim.SetBool("isAshJumping", true);
                startHeight = landHeight;

            }



            moveDir.y = moveDir.y + (Physics.gravity.y * gravityScale * Time.deltaTime);




            controller.Move(moveDir * Time.deltaTime);
            
           

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            {

                PauseGame();
                EventSystem.current.SetSelectedGameObject(GameObject.Find("ResumeButton"));

            }




        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && !hasDied)
        {

            PauseGame();

        }


    }

    public void SetCanMove(bool boolState)
    {
        canMove = boolState;
    }    

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(resumeButton);

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void QuitGame()
    {
        

        Application.Quit();
    }


    public void SetPlayerMaterial()
    {
        if (!useBaseMesh)
        {
            playerSkinMesh.material = burnedMaterial;

        }
        else
        {
            playerMesh.material = burnedMaterial;
        }
    }

    public void UpdateObjective(string objType)
    {
        if(objType == "Pedestal")
        {
            Scene scene = SceneManager.GetActiveScene();
            numOfPedestals = numOfPedestals - 1;

           







            if(scene.name == "LevelDesign1")
            {
                objectives.UpdateObjText("Totems Left To Light: " + numOfPedestals.ToString() + "\nLocate The Cave Entrance!");
            }
            else
            {
                objectives.UpdateObjText("Totems Left To Light: " + numOfPedestals.ToString());
            }

            checkpoint = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, playerCharacter.transform.position.z);
            
            if (numOfPedestals <= 0)
            {
                if (scene.name == "Cave")
                {
                    //                  stairsandwheels.SetActive(true);
                }
                else if (scene.name == "LevelDesign1")
                {
                    objectives.UpdateObjText("Enter the Cave and save your village!");
                }
                else if (scene.name == "Prototype")
                {
                    objectives.UpdateObjText("Return to the village!");
                    endLevelTrigger.SetActive(true);

                }
                else
                {
                    winText.SetActive(true);
                    hasWon = true;
                    isPaused = true;
                    anim.SetFloat("MoveForward", 0f);
                    anim.SetFloat("MoveRight", 0f);
                    fadeOut.SetBool("isLevelCompleted", true);
                }
                
            }

        }
        else if(objType == "StartingMission")
        {
            objectives.UpdateObjText("Leave town across the bridge and relight the Totem flames");
        }

    }

    

    public void ToggleSpeed(float value)
    {
        tempMoveSpeed = value;
        isBoosted = !isBoosted;
        if (isBoosted)
        {
            moveSpeed = moveSpeed * tempMoveSpeed;
        }

        else
        {
            moveSpeed = moveSpeedMaster;
        }



    }

    public void GiveFlameToPedestal()
    {
        isGivingFlame = true;
        //SetHealth(-1 * (health / 2));
    }

    public void PlayPickupAudio()
    {
        pickupAudio.Play();
    }
    public Vector3 getMoveDir(){
        return moveDir;
    }
}
