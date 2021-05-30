using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject Player;
    public GameObject elevGround;
    
    public float touchDist = 3.0f;

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
        Debug.Log("is you moving? " + elevatorMovementScript.moving);
        if(elevatorMovementScript.moving == true && Vector3.Distance(Player.transform.position, elevGround.transform.position) < touchDist)
        {
            Debug.Log("Player adopted");
            Player.transform.parent = elevGround.transform;
        }
        else
        {
            Debug.Log("Player orphaned");
            Player.transform.parent = null;
        }
    }
}
