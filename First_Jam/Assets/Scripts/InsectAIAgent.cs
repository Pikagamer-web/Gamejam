using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is RayCast Based AI. Just like Insect Antennas. Raycasts canbe assumed like an Antenna that can sense environment and
/// pass the information to the insect. Now its upto insect what to do with that info! InsectState hasnt been implemented yet! 
///Sensor, their sensitivity, log function and Quaternions saved d day!
/// </summary>


public enum InsectState { idle,movingStraightToTarget, avoidingLeft, avoidingRight, avoidingByRising, avoidingByDiving }

public class InsectAIAgent : MonoBehaviour
{
    public Transform LSensor, RSensor, CSensor;  // Controls x-z plane avoidances
    public float SideWaySensorsLength, CentralSensorLength;
    public Transform Graphics;
    public float HeightOfFlight,Speed;
    private bool LSensorActive, RSensorActive, CSensorActive;
    private RaycastHit LSHitInfo, RSHitInfo, CSHitInfo;
    [HideInInspector]public bool ControlFallbackToHorizontalSensors = false;
    public LayerMask groundMask, playerMask;
    private float LSensorSesitivity, RSensorSensitivity, AngleOfAvoidanceH;
    [HideInInspector]public Vector3 targetVector;
    [HideInInspector]public Transform target;
    [HideInInspector]public bool ReachedDestination = false;
    [HideInInspector]public InsectState insectState;
    [HideInInspector]public bool CanFly = false;
    private float HRotationSpeed, NRotationSpeed;
    private float powerH, powerN;
    [HideInInspector]public bool CanMoveForeward = true;

   [SerializeField] CharacterController cc;

    public bool isGrounded = false;
    float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        insectState = InsectState.movingStraightToTarget;
        LSensorActive = RSensorActive = CSensorActive = false;
        SetDestination(target.position);
        Debug.Log(targetVector);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!CanFly)
        {
            MaintainHeight();
        }

        SensorSetup();
        if (ControlFallbackToHorizontalSensors) { CommenceHorizontalAvoidance(); } else { CommenceBasicMovements(); }
       
        if (!ReachedDestination && CanMoveForeward) { /*transform.position += transform.forward * 0.06f*Speed*Time.deltaTime*20f;*/ cc.Move(new Vector3(transform.forward.x, transform.position.y, transform.forward.z) *Time.deltaTime*Speed);}
        UpdateGraphics();
    }

    void UpdateGraphics()
    {
        Graphics.rotation = Quaternion.Euler(Quaternion.identity.eulerAngles.x, transform.eulerAngles.y, Quaternion.identity.eulerAngles.z);
    }


    void MaintainHeight()
    {
        if (isGrounded)
        {
            time = 0.0f;
            return;
        }
        else
        {
            time += Time.deltaTime;
            cc.Move( new Vector3(0, transform.position.y - 9.8f * time * time,0));
        }
    }
    void SensorSetup()
    {
        RSensorActive = Physics.Raycast(RSensor.position, RSensor.forward, out RSHitInfo, SideWaySensorsLength );
        LSensorActive = Physics.Raycast(LSensor.position, LSensor.forward, out LSHitInfo, SideWaySensorsLength);
        CSensorActive = Physics.Raycast(CSensor.position, CSensor.forward, out CSHitInfo, CentralSensorLength);
        if (RSensorActive) { Debug.DrawLine(RSensor.position, RSHitInfo.point); }
        if (LSensorActive) { Debug.DrawLine(LSensor.position, LSHitInfo.point); }
        if (CSensorActive) { Debug.DrawLine(CSensor.position, CSHitInfo.point); }
        if(RSensorActive || LSensorActive || CSensorActive) { ControlFallbackToHorizontalSensors = true; } else { ControlFallbackToHorizontalSensors = false; }
        if (ControlFallbackToHorizontalSensors)
        {
            HRotationSpeed += Mathf.Pow(2f, powerH)*0.01f;
            powerH += 0.01f;
            HRotationSpeed = Mathf.Clamp(HRotationSpeed, 0, 0.3f);
            NRotationSpeed = 0;
            powerN = 0;
        }
        else
        {
            HRotationSpeed = 0;
            powerH = 0;

            NRotationSpeed += Mathf.Pow(2f, powerH)*0.01f ;
            powerN += 0.01f;
            NRotationSpeed = Mathf.Clamp(NRotationSpeed, 0, 0.3f);
        } 
    }

    void CommenceHorizontalAvoidance()
    {
        if (CSensorActive && !LSensorActive && !RSensorActive) { if (Vector3.Distance(transform.position, CSHitInfo.point) < 0.3f) { CanMoveForeward = false; } else { CanMoveForeward = true; } } else { CanMoveForeward = true; }
        if (LSensorActive) { LSensorSesitivity += (SideWaySensorsLength / Vector3.Distance(transform.position, LSHitInfo.point))*0.3f*Speed; } else { LSensorSesitivity = 0; }
        if (RSensorActive) { RSensorSensitivity -= (SideWaySensorsLength / Vector3.Distance(transform.position, RSHitInfo.point))*0.3f*Speed; } else { RSensorSensitivity = 0; }
        AngleOfAvoidanceH = LSensorSesitivity + RSensorSensitivity;
        Quaternion targetRot = transform.rotation * Quaternion.Euler(0, AngleOfAvoidanceH, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, HRotationSpeed * Speed * Time.deltaTime * 20f);
    }

    public void SetDestination(Vector3 destination)
    {
        targetVector = destination;
        ReachedDestination = false;
    }

   
    void CommenceBasicMovements()
    { 
       Quaternion targetRot = Quaternion.LookRotation(-transform.position + targetVector);
        if (CanFly)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRot.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z), NRotationSpeed * Speed * Time.deltaTime * 20f);
            if (Vector3.Distance(transform.position, targetVector) < 0.6f) { ReachedDestination = true; }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z), NRotationSpeed * Speed * Time.deltaTime * 20f);
            if (Vector3.Distance(transform.position, new Vector3(targetVector.x, transform.position.y, targetVector.z)) < 0.6f) { ReachedDestination = true; }
        }
    }
}
