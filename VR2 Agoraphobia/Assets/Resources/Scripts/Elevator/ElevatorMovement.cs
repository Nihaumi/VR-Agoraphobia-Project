using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{

    public bool isMoving;
    public bool moving;

    public GameObject elevator; //++
    public GameObject groundF;
    public GameObject FirstF;
    public GameObject SecondF;

    public float slideTime;
    public float t = 0.35f;
    public float speed;

    public float currentPos;
    public float groundFPos;
    public float FirstFPos;
    public float SecondFPos;
    public float endPosDown;
    public float endPosUp;

    GameObject ManagerOfButtons;
    ButtonManager btnManageScript;

    public int last_reached_floor = 0;
    public int last_selected_floor = 0;

    // Start is called before the first frame update
    void Start()
    {
        ManagerOfButtons = GameObject.Find("ButtonManager");
        btnManageScript = ManagerOfButtons.GetComponent<ButtonManager>();

        isMoving = false;
        speed = 1;
        currentPos = elevator.transform.position.y;
        groundFPos = groundF.transform.position.y;
        FirstFPos = FirstF.transform.position.y;
        SecondFPos = SecondF.transform.position.y;
        endPosDown = 0;
        endPosUp = 0;
        //Debug.Log("start:" + groundFPos);
        //Debug.Log("End:" + endPosDown);

    }


//  lr  ls 
//  0   0   
//  0   1
//  0   2
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            moving = UpdateElevatorPosition();
            if(!moving){
                last_reached_floor = last_selected_floor;
                Debug.Log("last selected: " + last_selected_floor);
                Debug.Log("last reached: " + last_reached_floor);
            }
        }
    }

    public void startMoving()
    {
        moving = true;
        Debug.Log("last selected(starting): " + last_selected_floor);
        last_selected_floor = btnManageScript.selectedAnimation;
        last_reached_floor = -1000;
        // figure out which level to go to
        if (btnManageScript.selectedAnimation == 0)
        {
            //Debug.Log("current: " + currentPos);
            endPosUp = groundFPos;

        }

        if (btnManageScript.selectedAnimation == 1)
        {
            // Debug.Log("current: " + currentPos);
            endPosUp = FirstFPos;
        }

        if (btnManageScript.selectedAnimation == 2)
        {
            //Debug.Log("current: " + currentPos);
            endPosUp = SecondFPos;
        }
    }

    public void stopMoving()
    {
        Debug.Log("stopMoving");
        moving = false;

        if (btnManageScript.selectedBtn != null)
        {
            Debug.Log("animationend");
            btnManageScript.DeactivateBtn(btnManageScript.selectedBtn);
        }     
    }

    public bool UpdateElevatorPosition()
    {
        //elevator.transform.position = new Vector3(elevator.transform.position.x, Mathf.Lerp(currentPos, endPosUp, slideTime), elevator.transform.position.z);
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, new Vector3(0,endPosUp,0), speed*Time.deltaTime);

        //Debug.Log("goal: " + endPosUp);
        //slideTime = [0..1]
        //slideTime += speed * t * Time.deltaTime;

        currentPos = elevator.transform.position.y;
        if (currentPos == endPosUp)
        {
            slideTime = 1;       
            stopMoving();
            return false;
        }
        /*else if (slideTime < 0)
        {
            slideTime = 0;
            return false;
        }*/
        return true;
    }
}

