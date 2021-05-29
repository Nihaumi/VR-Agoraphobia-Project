using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject Player;
    public GameObject elevGround;

    public GameObject elevatorMovementObject;
    public ElevatorMovement elevatorMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        elevGround = GameObject.Find("elev_floor");

        elevatorMovementObject = GameObject.Find("elevator");
        elevatorMovementScript = elevatorMovementObject.GetComponent<ElevatorMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(elevatorMovementScript.moving == true)
        {
            Player.transform.parent = elevGround.transform;
        }
    }
}
