using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //buttons to press inside elevator
    public GameObject btnLvl_0;
    public GameObject btnLvl_1;
    public GameObject btnLvl_2;
    public GameObject btnDoorToggle;
    public GameObject btnDoorStop;
    public GameObject btnStop;

    Color color_btnInactive = new Color32(255, 240, 237, 255);
    Color color_btnStopinactive = new Color32(219, 63, 59, 255);
    Color color_btnStopactive = new Color32(255, 7, 0, 255);

    //index finger of Player
    public GameObject indexFinger_R;

    //to check if one button is active and if animation is done
    public bool finishedAnimation;
    public bool btnIsActive;

    //accessable for elev movement
    public int selectedAnimation;
    public GameObject selectedBtn;

    //distances for buttons and finger
    public float touchDist = 0.15f;

    //slide script
    GameObject DoorSlider;
    DoorSlide doorSlide;
    public bool callSlide = false;

    //elevator movement script 
    GameObject ElevatorMovement;
    ElevatorMovement elevMovement;


    // Start is called before the first frame update
    void Start()
    {
        //door sliding access
        DoorSlider = GameObject.Find("Doors");
        doorSlide = DoorSlider.GetComponent<DoorSlide>();

        //elevMovement obj, script
        ElevatorMovement = GameObject.Find("elevator");
        elevMovement = ElevatorMovement.GetComponent<ElevatorMovement>();

        //get objects
        btnLvl_0 = GameObject.Find("0");
        btnLvl_1 = GameObject.Find("1");
        btnLvl_2 = GameObject.Find("2");
        btnDoorToggle = GameObject.Find("door");
        btnDoorStop = GameObject.Find("stopp door");
        btnStop = GameObject.Find("stop");

        indexFinger_R = GameObject.Find("b_r_index3");
        Debug.Assert(indexFinger_R != null);

        btnIsActive = false;
        finishedAnimation = true;
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

    void UpdateSelectedButton() {
        //check distances between buttons and finger
        Debug.Log("finger found");
        bool button_0_was_touched = Vector3.Distance(indexFinger_R.transform.position, btnLvl_0.transform.position) < touchDist;
        float dist_1 = Vector3.Distance(indexFinger_R.transform.position, btnLvl_1.transform.position);
        float dist_2 = Vector3.Distance(indexFinger_R.transform.position, btnLvl_2.transform.position);
        float dist_DoorToggle = Vector3.Distance(indexFinger_R.transform.position, btnDoorToggle.transform.position);
        float dist_DoorStop = Vector3.Distance(indexFinger_R.transform.position, btnDoorStop.transform.position);
        float dist_Stop = Vector3.Distance(indexFinger_R.transform.position, btnStop.transform.position);

        if (dist_Stop < touchDist)
        {
            Debug.Log("stopp");
            btnStop.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            elevMovement.stopMoving();
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
        if (dist_1 < touchDist)
        {
            Debug.Log("touch");
            selectedBtn = btnLvl_1;
            selectedAnimation = 1;
        }
        if (dist_2 < touchDist)
        {
            Debug.Log("touch");
            selectedBtn = btnLvl_2;
            selectedAnimation = 2;
        }
    }

    void ActivateBtn(GameObject btn)
    {
        btn.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        // check if door open
        if (doorSlide.isOpen)
        {
            // close if necessary
            doorSlide.CloseDoor();
        }

        // wait for it to be closed
        if (!doorSlide.doorsAreMoving)
        {
            elevMovement.startMoving();
        }

        finishedAnimation = false;
        btnIsActive = true;
    }

    public void DeactivateBtn(GameObject btn)
    {
        selectedBtn = null;
        StartCoroutine(deactivateDelay(btn));
    }

    IEnumerator deactivateDelay(GameObject btn)
    {
        yield return new WaitForSeconds(1);

        if (btn.name.Equals("stop"))
        {
            btn.GetComponent<Renderer>().material.SetColor("_Color", color_btnStopinactive);
        }

        else
        {
            btn.GetComponent<Renderer>().material.SetColor("_Color", color_btnInactive);
        }
        yield return new WaitForSeconds(1);

        Debug.Log("call auf true");
        
        if (! btn.name.Equals("stop"))
        {
            doorSlide.OpenDoor();
        }

        finishedAnimation = true;
        btnIsActive = false;
    }
    
}
