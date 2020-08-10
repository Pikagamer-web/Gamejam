using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool LookAtTargetEverytime;
    public float rotationDamping, translationDamping;
    public float mouseSensitivity;
    public Transform cam;
    float xRot;
    float mouseX, mouseY;
    int TouchId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // rot
        
        for (int i =0; i< Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 delPos = Input.touches[i].deltaPosition;
                    mouseX = delPos.x;
                    mouseY = delPos.y;
                }
                else
                {
                    mouseX = mouseY = 0f;
                }
            }
            else
            {
                mouseX = mouseY = 0;
            }

           

            





        }
        // mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        //mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        
        xRot-= mouseY;
        xRot = Mathf.Clamp(xRot, -35, 35);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler( xRot, transform.eulerAngles.y + mouseX , 0), rotationDamping);
        if (LookAtTargetEverytime) { cam.LookAt(target); }
    }
    // Update is called once per frame


    bool IsOnUI(Vector3 touchpos)
    {
        bool retval = false;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = touchpos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        for (int i =0; i< results.Count; i++)
        {
            if(results[i].gameObject.GetComponent<NotCamera>() == null)
            {
               // results
            }
        }
        return retval;
    }

    void LateUpdate()
    {
        // Set the pos
        Vector3 targetPos =  Vector3.MoveTowards(transform.position, target.position, translationDamping*Time.deltaTime);
        transform.position = targetPos;
        cam.localPosition = offset;
       

    }

    
}
