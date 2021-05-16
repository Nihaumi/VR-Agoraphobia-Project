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



    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        speed = 1;
        currentPos = elevator.transform.position.y;
        groundFPos = groundF.transform.position.y;
        FirstFPos = FirstF.transform.position.y;
        SecondFPos = SecondF.transform.position.y;
        endPosDown = 0;
        endPosUp = 0;
        Debug.Log("start:" + groundFPos);
        Debug.Log("End:" + endPosDown);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("current: " + currentPos);
            moving = true;
            endPosUp = groundFPos;

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("current: " + currentPos);
            moving = true;
            endPosUp = FirstFPos;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("current: " + currentPos);
            moving = true;
            endPosUp = SecondFPos;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            moving = !moving;
        }

        if (moving)
        {
            moving = ToggleElev();
        }
    }

    public bool ToggleElev()
    {
        //elevator.transform.position = new Vector3(elevator.transform.position.x, Mathf.Lerp(currentPos, endPosUp, slideTime), elevator.transform.position.z);
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, new Vector3(0,endPosUp,0), speed*Time.deltaTime);

        Debug.Log("goal: " + endPosUp);
        //slideTime = [0..1]
        //slideTime += speed * t * Time.deltaTime;

        currentPos = elevator.transform.position.y;
        if (currentPos == endPosUp)
        {
            slideTime = 1;
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

