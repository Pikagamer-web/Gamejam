using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f;
    float h, v;
    bool jumpPressed = false;
    public Transform camHolder;
    public float jumpForce= 20f;
    public bool isJumping = false;
    public int NoOfJumpingsAllowedInMidAir = 1, currentJumpNo = 0;
    public float RotationDamping = 0.3f;
  //  public Transform rewindCheckpointsHolder;
   // public Vector3[] RewindCheckpoints;

    public GroundCheck groundCheck;

    private void Start()
    {
      //  var holder  = rewindCheckpointsHolder.GetComponentsInChildren<Transform>();
      //  for(int i =0; i< holder.Length; i++)
      //  {
      //      RewindCheckpoints[i] = holder[i].position; 
      //  }
        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        

        // jump 
        if (jumpPressed && currentJumpNo < NoOfJumpingsAllowedInMidAir)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            ++currentJumpNo;
        }
        else if (groundCheck.IsGrounded)
        {
            isJumping = false;
            currentJumpNo = 0;
        }

        //Update rotation
        Quaternion targetRot = Quaternion.Euler(transform.eulerAngles.x, camHolder.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, RotationDamping);
    }
    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //rb.angularVelocity =  Vector3.zero;
        Vector3 move = transform.right * h *(speed/2f)*Time.deltaTime + transform.forward * v *speed*Time.deltaTime;
        move = new Vector3(move.x, rb.velocity.y, move.z);
         rb.velocity = move;
       // rb.MovePosition(transform.position+move*Time.deltaTime);
      

       
    }

   
}
