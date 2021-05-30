using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //buttons to press inside elevator
    public GameObject btnLvl_0;
    public GameObject btnLvl_1;
    public GameObject btnLvl_2;

    public List<GameObject> navButtons;
    
    public GameObject btnOpenDoor;
    public GameObject btnCloseDoor;
    public GameObject btnStop;

    //buttons in floors to call the elevator
    public GameObject btn_floor_0;
    public GameObject btn_floor_1;
    public GameObject btn_floor_2;

    Color color_btnInactive = new Color32(255, 240, 237, 255);
    Color color_btnStopinactive = new Color32(219, 63, 59, 255);
    Color color_btnStopactive = new Color32(255, 7, 0, 255);

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

    //findfinger script
    public GameObject FingerManager;
    public FindFinger find_index_Script;

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
        btn_floor_0 = GameObject.Find("btn_floor_0");
        btn_floor_1 = GameObject.Find("btn_floor_1");
        btn_floor_2 = GameObject.Find("btn_floor_2");

        navButtons = new List<GameObject>();
        navButtons.Add(btnLvl_0);
        navButtons.Add(btnLvl_1);
        navButtons.Add(btnLvl_2);
        navButtons.Add(btn_floor_0);
        navButtons.Add(btn_floor_1);
        navButtons.Add(btn_floor_2);

        btnOpenDoor = GameObject.Find("openDoor");
        btnCloseDoor = GameObject.Find("closeDoor");
        btnStop = GameObject.Find("stop");
        
        //find finger script
        FingerManager = GameObject.Find("FingerManager");
        find_index_Script = FingerManager.GetComponent<FindFinger>();

        btnIsActive = false;
        finishedAnimation = true;
        stopIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelectedButton();

        if (selectedBtn != null)
        {
            ActivateBtn(selectedBtn);
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
        bool button_floor_0_was_touched = false;
        bool button_floor_1_was_touched = false;
        bool button_floor_2_was_touched = false;

        //float dist_0 = Vector3.Distance(indexFinger.transform.position, btnLvl_0.transform.position);
        //check distances between buttons and finger
        //bool button_0_was_touched = (Vector3.Distance(indexFinger_R.transform.position, btnLvl_0.transform.position) < touchDist);       
        if ((Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btnLvl_0.transform.position) < touchDist) || (Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btnLvl_0.transform.position) < touchDist))
        {
            button_0_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btnLvl_1.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btnLvl_1.transform.position) < touchDist)
        {
            button_1_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btnLvl_2.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btnLvl_2.transform.position) < touchDist)
        {
            button_2_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btnOpenDoor.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btnOpenDoor.transform.position) < touchDist)
        {
            button_doorOpen_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btnCloseDoor.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btnCloseDoor.transform.position) < touchDist)
        {
            button_doorClose_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btnStop.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btnStop.transform.position) < touchDist)
        {
            button_stop_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btn_floor_0.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btn_floor_0.transform.position) < touchDist)
        {
            button_floor_0_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btn_floor_1.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btn_floor_1.transform.position) < touchDist)
        {
            button_floor_1_was_touched = true;
        }
        if (Vector3.Distance(find_index_Script.indexFinger_R.transform.position, btn_floor_2.transform.position) < touchDist || Vector3.Distance(find_index_Script.indexFinger_L.transform.position, btn_floor_2.transform.position) < touchDist)
        {
            button_floor_2_was_touched = true;
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

        if (button_floor_0_was_touched)
        {
            Debug.Log("touch");
            selectedBtn = btn_floor_0;
            selectedAnimation = 0;
        }

        if (button_floor_1_was_touched)
        {
            Debug.Log("touch");
            selectedBtn = btn_floor_1;
            selectedAnimation = 1;
        }

        if (button_floor_2_was_touched)
        {
            Debug.Log("touch");
            selectedBtn = btn_floor_2;
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

        if (selectedBtn && !selectedBtn.name.Equals("stop"))
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
        if(this.navButtons.Contains(btn) && stopIsActive)
        {
            btnStop.GetComponent<Renderer>().material.SetColor("_Color", color_btnStopinactive);
            stopIsActive = false;
        }

        // check if door open
        if (this.navButtons.Contains(btn) && doorSlideScript.isOpen)
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
        //wenn stop active soll er nicht bei stopmoving deaktiviert werden, sondern so lange aktiv bis anderer knopf gedrückt
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

        if (this.navButtons.Contains(btn) && !stopIsActive)
        {
            doorSlideScript.OpenDoor();
        }

        finishedAnimation = true;
        btnIsActive = false;
    }

}
