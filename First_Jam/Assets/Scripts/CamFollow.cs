using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool LookAtTargetEverytime;
    public float rotationDamping, translationDamping;
    public float mouseSensitivity;
    public Transform cam;
    float xRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // rot

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        
        xRot-= mouseY;
        xRot = Mathf.Clamp(xRot, -35, 35);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler( xRot, transform.eulerAngles.y + mouseX , 0), rotationDamping);
        if (LookAtTargetEverytime) { cam.LookAt(target); }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        // Set the pos
        Vector3 targetPos =  Vector3.MoveTowards(transform.position, target.position, translationDamping*Time.deltaTime);
        transform.position = targetPos;
        cam.localPosition = offset;
       

    }

    
}
