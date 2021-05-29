using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallButonManager : MonoBehaviour
{
    public GameObject call_button;
    public GameObject this_floor;
    public GameObject current_elevator_floor;
    public GameObject elevator_movement_object;
    public ElevatorMovement elevator_movement_script;

    public GameObject index_R;
    public GameObject index_L;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
//on every button:
//check if button has beem touched
//change button color
// if elevator already on this floor change color, open door
//if elevator not on this floor, tell elevator to move to this floor
//chnage btn color