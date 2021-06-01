using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    //elevatorMovement
    public GameObject elevator_movement_object;
    public ElevatorMovement elevator_movement_script;
    //finger
    public GameObject FingerManager;
    public FindFinger find_index_Script;
    //DoorSlide
    public GameObject door_slide_obj;
    public DoorSlide door_slide_script;

    public GameObject Player;
    public GameObject elevator;
    public bool player_is_inside_elev;

    //spheres
    public GameObject teleport_0;
    public GameObject teleport_1;
    public GameObject teleport_2;
    public GameObject teleport_elev; //set child of elevator
    //spawnpoints
    public GameObject spawn_0;
    public GameObject spawn_1;
    public GameObject spawn_2;
    public GameObject spawn_elev; //set child of elevator
    public GameObject closest_spawn;

    //floors
    public GameObject floor_0;
    public GameObject floor_1;
    public GameObject floor_2;

    public float dist_elev = 3f; //distance player to elev floor when standing inside
    public float touchDist = 0.5f; //min dist to "touch" obj

    public GameObject objectToCheck; //controll obj

    public float dist_spawn_elev;
    public float dist_spawn_0;
    public float dist_spawn_1;
    public float dist_spawn_2;

    // Start is called before the first frame update
    void Start()
    {
        //get scripts
        //finger
        FingerManager = GameObject.Find("FingerManager");
        find_index_Script = FingerManager.GetComponent<FindFinger>();
        //movement
        elevator_movement_object = GameObject.Find("elevator");
        elevator_movement_script = elevator_movement_object.GetComponent<ElevatorMovement>();
        //doors
        door_slide_obj = GameObject.Find("Doors");
        door_slide_script = door_slide_obj.GetComponent<DoorSlide>();

        //parenting
        teleport_elev.transform.parent = elevator.transform;
        spawn_elev.transform.parent = elevator.transform;

        GetClosestSpawn();

        isPlayerInsideElevator();

        //reset player 
        //Player.transform.position = spawn_0.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //if player touching ground elev -> spawn elev
        //else find closest spawn
        Debug.Log("player distance to elev ground: " + GetDistances(objectToCheck));
        GetClosestSpawn(); 
        //inside elev sphere = elev sphere
        if (player_is_inside_elev)
        {
            ToggleSphere(teleport_elev);
            if(GetDistances(teleport_elev) < touchDist)
            {
                ActiveSphere(teleport_elev);
            }
        }
        //TODO: make it so that selected telepot sphere depends on where üayer is(if inside telepot = elev telepot)
        //if player in elev  touching sphere -> player position = closest spawn position

    }

    //check if player inside elevator
   void isPlayerInsideElevator()
    {
        if (GetDistances(spawn_elev) < dist_elev)
        {
            player_is_inside_elev = true;
        }
        else
            player_is_inside_elev = false;
    }

    //check which spawn is closest
    GameObject GetClosestSpawn()
    {
        float[] distances = new float[3];
        distances[0] = GetDistances(spawn_0);
        distances[1] = GetDistances(spawn_1);
        distances[2] = GetDistances(spawn_2);
        //Debug.Log("distance u floor 0 = " + distances[0]);

        float min_dist = distances[0];

        for (int i = 1; i < distances.Length; i++)
        {
            if (distances[i] < min_dist)
            {
                min_dist = distances[i];
            }
        }

        if (min_dist == distances[0])
            closest_spawn = spawn_0;
        if (min_dist == distances[1])
            closest_spawn = spawn_1;
        if (min_dist == distances[2])
            closest_spawn = spawn_2;

        return closest_spawn;
    }

    //check distance between player & obj
    float GetDistances(GameObject obj)
    {
        return Vector3.Distance(Player.transform.position, obj.transform.position);
    }
    void ToggleSphere(GameObject sphere)
    {
        //inside elev
        if (door_slide_script.isOpen && !door_slide_script.doorsAreMoving)
        {
            sphere.SetActive(true);
        }

        if (!door_slide_script.isOpen)
        {
            sphere.SetActive(false);
        }
    }

    void ActiveSphere(GameObject sphere)
    {
        sphere.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }

}
//if floor btn active && elevator not moving && doors open...or floor btn active, elevator reached floor, doors open = ready
//set sphere active -> ransform = transform of floorbtn +2x + 2z
//if sphee touched, change color
//player transform = spawnpoint transform of elevator

//if elevator door open (inside) set active sphee (inside)
//if touched, change color
//player transform = transform of spawnpoint (oustide)

