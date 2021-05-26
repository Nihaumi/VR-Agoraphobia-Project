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
    public float dist_0;
    public float dist_1;
    public float dist_2;
    public float dist_DoorStop;
    public float dist_DoorToggle;
    // Start is called before the first frame update
    void Start()
    {
        btnLvl_0 = GameObject.Find("0");
        btnLvl_1 = GameObject.Find("1");
        btnLvl_2 = GameObject.Find("2");
        btnDoorToggle = GameObject.Find("door");
        btnDoorStop = GameObject.Find("stopp door");

        indexFinger_R = GameObject.Find("b_r_index3");

        btnIsActive = false;
        finishedAnimation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!indexFinger_R)
        {
            indexFinger_R = GameObject.Find("b_r_index3");
        }
        if (indexFinger_R)
        {
            //check distances between buttons and finger
            Debug.Log("finger found");
            dist_0 = Vector3.Distance(indexFinger_R.transform.position, btnLvl_0.transform.position);
            dist_1 = Vector3.Distance(indexFinger_R.transform.position, btnLvl_1.transform.position);
            dist_2 = Vector3.Distance(indexFinger_R.transform.position, btnLvl_2.transform.position);
            dist_DoorToggle = Vector3.Distance(indexFinger_R.transform.position, btnDoorToggle.transform.position);
            dist_DoorStop = Vector3.Distance(indexFinger_R.transform.position, btnDoorStop.transform.position);

            //check if no btn is active and animations are finished and which button has been "touched"
            if(finishedAnimation && !btnIsActive)
            {
                Debug.Log("hi");
                if(dist_0 < touchDist)
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
                if (selectedBtn != null)
                {
                    ActivateBtn(selectedBtn);
                }

            }
            
        }        
    }
    void ActivateBtn(GameObject btn)
    {
        btn.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        finishedAnimation = false;
        btnIsActive = true;
    }

    public void DeactivateBtn(GameObject btn)
    {
        StartCoroutine(deactivateDelay(btn));
    }

    IEnumerator deactivateDelay(GameObject btn)
    {
        yield return new WaitForSeconds(2);
        btn.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        finishedAnimation = true;
        btnIsActive = false;
    }

}
