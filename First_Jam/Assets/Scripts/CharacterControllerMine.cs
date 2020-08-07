using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState { idle, speedingUP, turningLeft, turningRight}


public class CharacterControllerMine : MonoBehaviour
{

    CharacterController cc;
    public GroundCheck groundCheck;
    public float speed;
    bool jumpPressed = false;
    public Transform camHolder;
    public float RotationDamping = 0.3f;
    public float jumpHeight;
    public int NoOfJumpsInMidAirAllowed = 1;
    public int currentJumpNo;
    bool isJumping = false;
    Vector3 playerVel = Vector3.zero;
    
    //_____________________________________________

    public Transform ForceField; //Hides u from aliens
    public Material ForceFieldRingMat, ForceFieldHitMat;
    public float fieldStrength;   // If strength run out the alienbs ll detect and corrupt your chip within a certain amount of  time. If u have rewindTime and rewind ansd set everything back tonormal
    public float RewindTimeBar;  // The more clock gets rewind the more the time bar becomes. 
    public float chipCorruption; // After the field is destroyed

    float angle = 0;
    public bool IsHittingWithBots = false;
    int matSwitch = 0;
    int matswitch2 = 0;
    Renderer rend;
    public bool HasCorruptionStarted = false;

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public CheckPoint lastCheckpoint;
    public bool IsRewinding = false;

    //___________________________________________
    public ScifiClock currentClock;

    //__________________________________________

    public PlayerState playerState;
    [SerializeField] Animator GraphicsAnimator;

    //___________________________________

    public float noOfSecCanBeRewinded;
    public RewindableObject currentREwindTarget;

    //__________________________________

    [SerializeField] GameObject gameOverImage, credits;
    [SerializeField] GameObject gameOverCanvas;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        rend = ForceField.GetComponent<Renderer>();
        playerState = PlayerState.idle;
        gameOverImage.SetActive(false);
        credits.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRewindingSeconds();
        if (!IsRewinding)
        {
            Movement();
        }
        else
        {
            PerformRewinding();
        }
       
        ForceFieldBehaviour();
        RewindTime();
    }

    private void CalculateRewindingSeconds()
    {
        noOfSecCanBeRewinded = 6f;
    }

    private void PerformRewinding()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastCheckpoint.position, 20f * Time.deltaTime);
        if(Vector2.Distance(transform.position, lastCheckpoint.position) < 1f)
        {
            if (!ForceField.gameObject.activeInHierarchy) { ForceField.gameObject.SetActive(true); }
            fieldStrength = lastCheckpoint.c_FieldStrength;
            RewindTimeBar = 0;
            chipCorruption = 0;
            HasCorruptionStarted = false;
            IsRewinding = false;
            matSwitch = 0;
            IsHittingWithBots = false;
        }
    }

    private void RewindTime()
    {
        /*  if(RewindTimeBar >= 100 && Input.GetKeyDown(KeyCode.X) && lastCheckpoint!=null)
          {
              RewindTimeBar = 0;
              IsRewinding = true;
              //Rewinding Effect. its an event!

          }*/

        if (RewindTimeBar >= 20f && Input.GetMouseButtonDown(0)) // // We have to rewind the objects and self rather than the time itswelf a sa whole
        {
            if (currentREwindTarget != null)
            {
                currentREwindTarget.hasStaretedRewinding = true;
                RewindTimeBar -= 20f;
            }
            
        }     
    }

    private void ForceFieldBehaviour()
    {
        //RotationBehaviour (If not hit with bots)
        if (!IsHittingWithBots)
        {
            if (matSwitch == 0)
            {
                rend.material = ForceFieldRingMat;
                matSwitch = 1;
                matswitch2 = 0;
                ForceField.transform.rotation = Quaternion.identity;
            }
            angle += Time.deltaTime * 0.3f;
            if (angle > 360) { angle = 0; }
            ForceField.Rotate(Vector3.up, angle);
        }
        else
        {
            if(matswitch2 == 0)
            {
                rend.material = ForceFieldHitMat;
                matswitch2 = 1;
                matSwitch = 0;
            }
            //Rotate Inside OncollisionEnter, stay

            if (fieldStrength < 0)
            {
                ForceField.gameObject.SetActive(false);
                HasCorruptionStarted = true;
            }
        }

        if (HasCorruptionStarted)
        {
            chipCorruption += 0.5f;
            if (chipCorruption > 100f)
            {
                OnGameOver();
            }
        }

    }

    private void OnGameOver()
    {
        gameOverCanvas.SetActive(true);
        gameOverImage.SetActive(true);
        Invoke("ShowCredits", 1f);
       
    }
    void ShowCredits()
    {
        credits.SetActive(true);
        Invoke("LoadMenu", 5f);
    }
    void LoadMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        //____________________
        if (v > 0 ) { playerState = PlayerState.speedingUP; }
        if( h > 0) { playerState = PlayerState.turningRight; }
        if (h < 0) { playerState = PlayerState.turningLeft; }
        if(h==0 && v == 0) { playerState = PlayerState.idle; }

        GraphicsAnimator.SetInteger("StateIndex", (int)playerState);
        //_____________________
        if (playerVel.y < 0 && groundCheck.IsGrounded) { playerVel.y = 0; }
        Vector3 move = transform.right * h + transform.forward * v;
        move = move.normalized;
        cc.Move(move * Time.deltaTime * speed);
        Quaternion targetRot = Quaternion.Euler(transform.eulerAngles.x, camHolder.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, RotationDamping);

        if (groundCheck.IsGrounded)
        {
            isJumping = false;
            currentJumpNo = 0;
        }
        if (jumpPressed && currentJumpNo < NoOfJumpsInMidAirAllowed)
        {
            isJumping = true;
            currentJumpNo += 1;

            playerVel.y += Mathf.Sqrt(2 * 9.8f * jumpHeight);
        }



        playerVel.y -= 9.8f * Time.deltaTime;
        cc.Move(playerVel * Time.deltaTime);
    }
}
