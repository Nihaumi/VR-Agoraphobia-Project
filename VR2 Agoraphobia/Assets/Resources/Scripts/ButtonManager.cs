using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //buttons to press inside elevator
    public GameObject btnLvl_0;
    public GameObject btnLvl_1;
    public GameObject btnLvl_2;
    public GameObject btnOpenDoor;
    public GameObject btnCloseDoor;
    public GameObject btnStop;

    Color color_btnInactive = new Color32(255, 240, 237, 255);
    Color color_btnStopinactive = new Color32(219, 63, 59, 255);
    Color color_btnStopactive = new Color32(255, 7, 0, 255);

    //index finger of Player
    public GameObject indexFinger_R;
    public GameObject indexFinger_L;

    //to check if one button is active and if animation is done
    public bool finishedAnimation;
    public bool btnIsActive;

    //check if stop btn active 
    public bool stopIsActive;

    //accessable for elev movement
    public int selectedAnimation;
    public GameObject selectedBtn;

    //distance for buttons and finger
    public float touchDist = 0.15f;

    //slide script
    GameObject DoorSliderObj;
    DoorSlide doorSlideScript;
    public bool callSlide = false;

    //elevator movement script 
    GameObject ElevatorMovement;
    ElevatorMovement elevMovement;


    // Start is called before the first frame update
    void Start()
    {
        //door sliding access
        DoorSliderObj = GameObject.Find("Doors");
        doorSlideScript = DoorSliderObj.GetComponent<DoorSlide>();

        //elevMovement obj, script
        ElevatorMovement = GameObject.Find("elevator");
        elevMovement = ElevatorMovement.GetComponent<ElevatorMovement>();

        //get objects
        btnLvl_0 = GameObject.Find("0");
        btnLvl_1 = GameObject.Find("1");
        btnLvl_2 = GameObject.Find("2");
        btnOpenDoor = GameObject.Find("openDoor");
        btnCloseDoor = GameObject.Find("closeDoor");
        btnStop = GameObject.Find("stop");

        FindFinger();

        btnIsActive = false;
        finishedAnimation = true;
        stopIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        FindFinger();
        UpdateSelectedButton();

        if (selectedBtn != null)
        {
            ActivateBtn(selectedBtn);
        }
    }

    void FindFinger()
    {
        if (indexFinger_L == null || indexFinger_R == null)
        {
            indexFinger_R = GameObject.Find("b_r_index3");
            Debug.Assert(indexFinger_R != null, "no index R");
            indexFinger_L = GameObject.Find("b_l_index3");
            Debug.Assert(indexFinger_L != null, "ni index L");
        }
    }

    void UpdateSelectedButton()
    {
        bool button_0_was_touched = false;
        bool button_1_was_touched = false;
        bool button_2_was_touched = false;
        bool button_stop_was_touched = false;
        bool button_doorOpen_was_touched = false;
        bool button_doorClose_was_touched = false;

        //float dist_0 = Vector3.Distance(indexFinger.transform.position, btnLvl_0.transform.position);
        //check distances between buttons and finger
        //bool button_0_was_touched = (Vector3.Distance(indexFinger_R.transform.position, btnLvl_0.transform.position) < touchDist);
        if ((Vector3.Distance(indexFinger_R.transform.position, btnLvl_0.transform.position) < touchDist) || (Vector3.Distance(indexFinger_L.transform.position, btnLvl_0.transform.position) < touchDist))
        {
            button_0_was_touched = true;
        }
        if (Vector3.Distance(indexFinger_R.transform.position, btnLvl_1.transform.position) < touchDist || Vector3.Distance(indexFinger_L.transform.position, btnLvl_1.transform.position) < touchDist)
        {
            button_1_was_touched = true;
        }
        if (Vector3.Distance(indexFinger_R.transform.position, btnLvl_2.transform.position) < touchDist || Vector3.Distance(indexFinger_L.transform.position, btnLvl_2.transform.position) < touchDist)
        {
            button_2_was_touched = true;
        }
        if (Vector3.Distance(indexFinger_R.transform.position, btnOpenDoor.transform.position) < touchDist || Vector3.Distance(indexFinger_L.transform.position, btnOpenDoor.transform.position) < touchDist)
        {
            button_doorOpen_was_touched = true;
        }
        if (Vector3.Distance(indexFinger_R.transform.position, btnCloseDoor.transform.position) < touchDist || Vector3.Distance(indexFinger_L.transform.position, btnCloseDoor.transform.position) < touchDist)
        {
            button_doorClose_was_touched = true;
        }
        if (Vector3.Distance(indexFinger_R.transform.position, btnStop.transform.position) < touchDist || Vector3.Distance(indexFinger_L.transform.position, btnStop.transform.position) < touchDist)
        {
            button_stop_was_touched = true;
        }

        //stop button check
        if (button_stop_was_touched)
        {
            ActivateStop();
        }

        //check if no btn is active and animations are finished and which button has been "touched"
        if (!finishedAnimation || btnIsActive)
            return;

        if (button_0_was_touched)
        {
            Debug.Log("touch");
            selectedBtn = btnLvl_0;
            selectedAnimation = 0;
        }
        if (button_1_was_touched)
        {
            Debug.Log("touch");
            selectedBtn = btnLvl_1;
            selectedAnimation = 1;
        }
        if (button_2_was_touched)
        {
            Debug.Log("touch");
            selectedBtn = btnLvl_2;
            selectedAnimation = 2;
        }
        //close door
        //wait for door to be closed
        if (button_doorClose_was_touched)
        {
            selectedBtn = btnCloseDoor;
            Debug.Log("closig oasdnjosandosan" + doorSlideScript.isOpen);
            doorSlideScript.CloseDoor();
            if (!doorSlideScript.doorsAreMoving)
            {
                DeactivateBtn(btnCloseDoor);
            }
        }

        if (button_doorOpen_was_touched)
        {
            selectedBtn = btnOpenDoor;
            doorSlideScript.OpenDoor();
            if (!doorSlideScript.doorsAreMoving)
            {
                DeactivateBtn(btnOpenDoor);
            }
        }
    }


    void ActivateStop()
    {
        stopIsActive = true;

        if (!selectedBtn.name.Equals("stop"))
        {
            selectedBtn.GetComponent<Renderer>().material.SetColor("_Color", color_btnInactive);
        }

        btnStop.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        elevMovement.stopMoving();
    }

    void ActivateBtn(GameObject btn)
    {
        Debug.Log("activate");
        btn.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        //wenn stop button aktiv dann stopp button deaktivieren FALLS es ein floor btn ist, bei door open/close bleibt stop active
        if(stopIsActive && !btn.name.Equals("openDoor") && !btn.name.Equals("closeDoor"))
        {
            btnStop.GetComponent<Renderer>().material.SetColor("_Color", color_btnStopinactive);
            stopIsActive = false;
        }

        // check if door open
        if (doorSlideScript.isOpen && !btn.name.Equals("openDoor") && !btn.name.Equals("closeDoor"))
        {
            // close if necessary
            doorSlideScript.CloseDoor();
        }

        // wait for it to be closed
        if (!doorSlideScript.doorsAreMoving)
        {
            elevMovement.startMoving();
        }

        finishedAnimation = false;
        btnIsActive = true;
    }

    public void DeactivateBtn(GameObject btn)
    {
        if (GameObject.ReferenceEquals(btn, selectedBtn))
            selectedBtn = null;
    
        StartCoroutine(deactivateDelay(btn));
    }

    IEnumerator deactivateDelay(GameObject btn)
    {
        //wenn stop active soll er nicht bei stopmoving deaktiviert werden, sondern so lange aktiv bis anderer knopf gedr�ckt
        /*if (btn.name.Equals("stop"))
        {
            btn.GetComponent<Renderer>().material.SetColor("_Color", color_btnStopinactive);
        }

        else*/
        if (!btn.name.Equals("stop"))
        {
            Debug.Log("inactive btn color for: " + btn);
            btn.GetComponent<Renderer>().material.SetColor("_Color", color_btnInactive);
        }
        yield return new WaitForSeconds(1);

        if (!btn.name.Equals("stop") && !btn.name.Equals("openDoor") && !btn.name.Equals("closeDoor"))
        {
            doorSlideScript.OpenDoor();
        }

        finishedAnimation = true;
        btnIsActive = false;
    }

}
