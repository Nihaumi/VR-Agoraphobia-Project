using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{

    //Colormanager
    GameObject ColorManager_obj;
    ColorManager ColorManager_script;

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
    public bool sphere_is_touched;

    //spheres
    public GameObject teleport_0;
    public GameObject teleport_1;
    public GameObject teleport_2;
    public GameObject teleport_elev; //set child of elevator
    public GameObject choosen_sphere;
    //spawnpoints
    public GameObject spawn_0;
    public GameObject spawn_1;
    public GameObject spawn_2;
    public GameObject spawn_elev; //set child of elevator
    public GameObject closest_spawn;


    public float dist_elev = 3.8f; //distance player to elev floor when standing inside
    public float touchDist = 0.5f; //min dist to "touch" obj

    public GameObject objectToCheck; //controll obj
    public float player_dist_to_elev_floor; //for testing
    public float turning_angle = 115f; //test
    public float dist_spawn_elev;
    public float dist_spawn_0;
    public float dist_spawn_1;
    public float dist_spawn_2;

    // Start is called before the first frame update
    void Start()
    {
        //Colormanager obj, script
        ColorManager_obj = GameObject.Find("ColorManager");
        ColorManager_script = ColorManager_obj.GetComponent<ColorManager>();

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

        sphere_is_touched = false;

        //reset player 
        //Player.transform.position = spawn_0.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInsideElevator();
        //if player touching ground elev -> spawn elev
        //else find closest spawn
        GetClosestSpawn();  //has a return value and sets the global var at the same time.

        dist_spawn_elev = GetDistances(Player, spawn_elev);
        //actives the chosen teleport sphere
        ToggleSphere(ChooseTeleportSphere());
        if (!sphere_is_touched &&
            (GetDistances(find_index_Script.indexFinger_L, choosen_sphere) < touchDist
            || GetDistances(find_index_Script.indexFinger_R, choosen_sphere) < touchDist)
        )
        {
            sphere_is_touched = true;
            ActiveSphere(choosen_sphere);
        }
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

    GameObject ChooseTeleportSphere() //the sphere to trigger to teleport
    {
        if (player_is_inside_elev)
        {
            choosen_sphere = teleport_elev;
        }

        else
        {
            if (elevator_movement_script.last_reached_floor == 0)
            {
                choosen_sphere = teleport_0;
            }
            if (elevator_movement_script.last_reached_floor == 1)
            {
                choosen_sphere = teleport_1;
            }
            if (elevator_movement_script.last_reached_floor == 2)
            {
                choosen_sphere = teleport_2;
            }
        }
        return choosen_sphere;
    }

    void ActiveSphere(GameObject sphere)
    {
        sphere.GetComponent<Renderer>().material.SetColor("_Color", ColorManager_script.color_btnActive);
        if (sphere.activeSelf)
        {
            StartCoroutine(ActivateCoroutine(sphere));
        }
    }

    IEnumerator ActivateCoroutine(GameObject sphere)
    {
        yield return new WaitForSeconds(0.3f);
        Player.transform.position = closest_spawn.transform.position;
        if (GameObject.ReferenceEquals(closest_spawn, spawn_elev))
        {
            turning_angle = 115;
        }
        else
            turning_angle = 0;
            
        Player.transform.rotation = Quaternion.Euler(0, turning_angle, 0);
        sphere.GetComponent<Renderer>().material.SetColor("_Color", ColorManager_script.color_btnInactive);
        sphere.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        sphere_is_touched = false;
    }

    //check if player inside elevator
    void isPlayerInsideElevator()
    {
        if (GetDistances(Player, spawn_elev) < dist_elev)
        {
            player_is_inside_elev = true;
        }
        else
            player_is_inside_elev = false;
        Debug.Log("Is player inside elevator? " + player_is_inside_elev);
    }

    //check which spawn is closest
    GameObject GetClosestSpawn()
    {
        if (!player_is_inside_elev)
        {
            closest_spawn = spawn_elev;
        }
        else
        {
            float[] distances = new float[3];
            distances[0] = GetDistances(Player, spawn_0);
            distances[1] = GetDistances(Player, spawn_1);
            distances[2] = GetDistances(Player, spawn_2);
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
        }

        return closest_spawn;
    }

    //check distance between 2 obj
    float GetDistances(GameObject obj_1, GameObject obj_2)
    {
        return Vector3.Distance(obj_1.transform.position, obj_2.transform.position);
    }
}
//if floor btn active && elevator not moving && doors open...or floor btn active, elevator reached floor, doors open = ready
//set sphere active -> ransform = transform of floorbtn +2x + 2z
//if sphee touched, change color
//player transform = spawnpoint transform of elevator

//if elevator door open (inside) set active sphee (inside)
//if touched, change color
//player transform = transform of spawnpoint (oustide)

