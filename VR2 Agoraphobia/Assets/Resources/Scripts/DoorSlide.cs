using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSlide : MonoBehaviour
{

    public bool isOpen;
    public bool doorsAreMoving;

    public GameObject doorLeft; //--
    public GameObject doorRight; //++

    public float slideTime;
    public float t = 0.35f;

    public float startPosL;
    public float endPosL;

    public float startPosR;
    public float endPosR;

    //Button Manager scipt
    GameObject ManagerOfButtons;
    ButtonManager btnManageScript;

    // Start is called before the first frame update
    void Start()
    {
        ManagerOfButtons = GameObject.Find("ButtonManager");
        btnManageScript = ManagerOfButtons.GetComponent<ButtonManager>();

        isOpen = false;
        startPosL = doorLeft.transform.position.z;
        endPosL = startPosL - 2;

        startPosR = doorRight.transform.position.z;
        endPosR = startPosR + 2;
        //Debug.Log("start:" + startPosL);
        //Debug.Log("End:" + endPosL);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOpen)
                CloseDoor();
            else
                OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            doorsAreMoving = !doorsAreMoving;
        }

        if (doorsAreMoving)
        {
            Debug.Log("enter doorsAreMoving");
            doorsAreMoving = UpdateDoorPosition();
        }
    }

    public bool UpdateDoorPosition()
    {
        Debug.Log("UpdateDoorPosition");

        doorLeft.transform.position = new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, Mathf.Lerp(startPosL, endPosL, slideTime));
        doorRight.transform.position = new Vector3(doorRight.transform.position.x, doorRight.transform.position.y, Mathf.Lerp(startPosR, endPosR, slideTime));
        //slideTime = [0..1]

       int direction = 1;
        if (isOpen == false)
        {
            direction = -1;
        }

        slideTime += direction * t * Time.deltaTime;

        if (slideTime > 1)
        {
            slideTime = 1;
            doorsAreMoving = false;
            return false;
        }
        else if (slideTime < 0)
        {
            slideTime = 0;
            doorsAreMoving = false;
            return false;
        }
        return true;
    }

    public void OpenDoor()
    {
        // assert that doors are closed
        Debug.Assert(!this.isOpen, "Door was open, but was supposed to open.");

        // signal doors should open
        doorsAreMoving = true;

        this.isOpen = true;
    }

    public void CloseDoor()
    {
        // assert that doors are open
        Debug.Assert(this.isOpen, "Door was closed, but was supposed to close.");

        // signal doors should close
        doorsAreMoving = true;

        this.isOpen = false;
    }
}
