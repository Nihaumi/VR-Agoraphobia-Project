using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    //floors
    public GameObject floor_0;
    public GameObject floor_1;
    public GameObject floor_2;

    //spheres
    public GameObject teleport_0;
    public GameObject teleport_1;
    public GameObject teleport_2;

    //floor Btn
    public GameObject floor_btn_0;
    public GameObject floor_btn_1;
    public GameObject floor_btn_2;

    //offset
    public float offset_btn = 2f;
    public float offset_sphere = 2f;


    // Start is called before the first frame update
    void Start()
    {
        //get pbjects
        floor_0 = GameObject.Find("lvl");
        floor_1 = GameObject.Find("lvl1");
        floor_2 = GameObject.Find("lvl2");

        teleport_0 = GameObject.Find("sphere_0");
        teleport_1 = GameObject.Find("sphere_1");
        teleport_2 = GameObject.Find("sphere_2");

        floor_btn_0 = GameObject.Find("call_0");
        floor_btn_1= GameObject.Find("call_1");
        floor_btn_2= GameObject.Find("call_2");

    }

    // Update is called once per frame
    void Update()
    {
        teleport_0.transform.position = new Vector3(0, floor_0.transform.position.y + offset_sphere, 0);
        teleport_1.transform.position = new Vector3(0, floor_1.transform.position.y + offset_sphere, 0);
        teleport_2.transform.position = new Vector3(0, floor_2.transform.position.y + offset_sphere, 0);

        floor_btn_0.transform.position = new Vector3(0, floor_0.transform.position.y + offset_btn, 0);
        floor_btn_1.transform.position = new Vector3(0, floor_1.transform.position.y + offset_btn, 0);
        floor_btn_2.transform.position = new Vector3(0, floor_2.transform.position.y + offset_btn, 0);
    }
}
