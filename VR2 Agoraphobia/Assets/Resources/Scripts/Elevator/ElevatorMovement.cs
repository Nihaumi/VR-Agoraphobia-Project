using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    public static int ANIM_FLOOR_0 = 0;
    public static int ANIM_FLOOR_1 = 1;
    public static int ANIM_FLOOR_2 = 2;

    public static int ANIM_SHAKE = 3;

    public bool isMoving;
    public bool moving;
    public bool isShaking;

    public GameObject elevator; //++
    public GameObject elev_floor;
    public GameObject groundF;
    public GameObject FirstF;
    public GameObject SecondF;

    public float slideTime;
    public float t = 0.35f;
    public float speed;
    public float y_before_shaking;
    public float shake_start_time;

    public float currentPos;
    public float groundFPos;
    public float FirstFPos;
    public float SecondFPos;
    public float endPosDown;
    public float endPosUp;
    public float offset_to_floor = 0.2f;

    public float last_reached_floor = 0;
    public float last_selected_floor = 0;

    //sound script
    public GameObject sound_obj;
    public Sound sound_script;

    //btn manager script
    GameObject ManagerOfButtons;
    ButtonManager btnManageScript;

    // Start is called before the first frame update
    void Start()
    {
        //scripts
        sound_obj = GameObject.Find("AudioManager_elev");
        sound_script = sound_obj.GetComponent<Sound>();

        ManagerOfButtons = GameObject.Find("ButtonManager");
        btnManageScript = ManagerOfButtons.GetComponent<ButtonManager>();

        isMoving = false;
        isShaking = false;
        shake_start_time = 0;
        speed = 1;
        currentPos = elevator.transform.position.y;
        groundFPos = groundF.transform.position.y + offset_to_floor;
        FirstFPos = FirstF.transform.position.y + offset_to_floor;
        SecondFPos = SecondF.transform.position.y + offset_to_floor;
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
            //play sound
          /*  if (!sound_script.sound_is_playing)
            {
                sound_script.PlaySound(sound_script.moving);
                sound_script.moving.loop = true;
            }*/

            moving = UpdateElevatorPosition();
           
            if (!moving)
            {
                last_reached_floor = last_selected_floor;
               // sound_script.StartCoroutine("HandleStopSounds");

                // Debug.Log("last selected: " + last_selected_floor);
                //Debug.Log("last reached: " + last_reached_floor);
            }

        }

        else if(isShaking)
        {
            UpdateShake();
        }

    }

    public void startMoving()
    {
        moving = true;
        //Debug.Log("last selected(starting): " + last_selected_floor);
        last_selected_floor = btnManageScript.selectedAnimation;
        last_reached_floor = -1000;

        // figure out which level to go to
        if (btnManageScript.selectedAnimation == ANIM_FLOOR_0)
        {
            //Debug.Log("current: " + currentPos);
            endPosUp = groundFPos;

        }

        if (btnManageScript.selectedAnimation == ANIM_FLOOR_1)
        {
            // Debug.Log("current: " + currentPos);
            endPosUp = FirstFPos;
        }

        if (btnManageScript.selectedAnimation == ANIM_FLOOR_2)
        {
            //Debug.Log("current: " + currentPos);
            endPosUp = SecondFPos;
        }
    }

    public void StartShaking()
    {
        // change y-coordinates +0.4 - 0.4
        //for 2 seconds transform.position += Vector3.up * Time.deltaTime;

        if (isShaking)
            return;

        y_before_shaking = elev_floor.transform.position.y;
        shake_start_time = Time.time;
        
        isShaking = true;
    }
    
    void UpdateShake()
    {
        if (Time.time >= shake_start_time + 2)
            stopShaking();
        else
            elev_floor.transform.position += Vector3.up * Mathf.Cos(Time.time);
    }

    public void stopShaking()
    {
        elev_floor.transform.position = new Vector3(elev_floor.transform.position.x, y_before_shaking, elev_floor.transform.position.z);
        isShaking = false;
        startMoving();
    }

    public void stopMoving()
    {
        //Debug.Log("stopMoving");
        moving = false;
       // StartCoroutine("HandleStopSounds");

        if (btnManageScript.selectedBtn != null)
        {
          //  Debug.Log("animationend");
            btnManageScript.DeactivateBtn(btnManageScript.selectedBtn);
        }
    }

    public bool UpdateElevatorPosition()
    {
        //elevator.transform.position = new Vector3(elevator.transform.position.x, Mathf.Lerp(currentPos, endPosUp, slideTime), elevator.transform.position.z);
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, new Vector3(0, endPosUp, 0), speed * Time.deltaTime);

        //Debug.Log("goal: " + endPosUp);
        //slideTime = [0..1]
        //slideTime += speed * t * Time.deltaTime;
      //  Debug.Log("\tgoal: " + endPosUp + "\tcurr:" + currentPos);

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

