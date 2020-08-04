using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        rend = ForceField.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ForceFieldBehaviour();
        RewindTime();
    }

    private void RewindTime()
    {
        if(RewindTimeBar >= 100 && Input.GetKeyDown(KeyCode.X))
        {
            RewindTimeBar = 0;
            //Rewinding Effect
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
        throw new NotImplementedException();
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);

        if (playerVel.y < 0 && groundCheck.IsGrounded) { playerVel.y = 0; }
        Vector3 move = transform.right * h + transform.forward * v;

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
