using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    //slide script
    GameObject DoorSliderObj;
    DoorSlide doorSlideScript;

    //elevator movement script 
    GameObject ElevatorMovementGameObject;
    ElevatorMovement elev_movement_script;

    //btn manager script
    GameObject ManagerOfButtons;
    ButtonManager btnManageScript;

    public AudioSource door_opening;
    public AudioSource door_closing;
    public AudioSource start_moving;
    public AudioSource stop_moving;
    public AudioSource moving;
    public AudioSource button_press;

    public bool sound_is_playing;
    public AudioSource current_audio_source;
    public float fade_time;
    public float start_door_opening;
    public float start_door_closing;

    public AudioSource door_audio;
    public float sliding_time;
    public float time_door_opened;
    public float time_door_closed;
    public bool stop_activated;
    // Start is called before the first frame update
    void Start()
    {
        //door sliding access
        DoorSliderObj = GameObject.Find("Doors");
        doorSlideScript = DoorSliderObj.GetComponent<DoorSlide>();

        //elevMovement obj, script
        ElevatorMovementGameObject = GameObject.Find("elevator");
        elev_movement_script = ElevatorMovementGameObject.GetComponent<ElevatorMovement>();

        //btn manager script
        ManagerOfButtons = GameObject.Find("ButtonManager");
        btnManageScript = ManagerOfButtons.GetComponent<ButtonManager>();

        start_door_opening = 0.05f;
        start_door_closing = 0.95f;
        fade_time = 0.5f;
        time_door_closed = 0.01f;
        time_door_opened = 0.9f;
        sound_is_playing = false;
        stop_activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        sliding_time = doorSlideScript.slideTime;
        Debug.Log("HELOOOOO");


        if (elev_movement_script.moving && !doorSlideScript.doorsAreMoving)
        {
            Debug.Log("moving SOUND");
            if (!sound_is_playing)
            {
                door_audio = moving;
                PlaySound(door_audio);
            }
        }
        else if ((current_audio_source.name == "moving" 
            && sound_is_playing
            && elev_movement_script.last_reached_floor == elev_movement_script.last_selected_floor))
            /*|| (btnManageScript.stopIsActive /*&& !stop_activated)))*/
        {
            Debug.Log("YES IT WORKS");
            StartCoroutine(HandleStopSounds());
            stop_activated = true;
        }

        /*if (!btnManageScript.stopIsActive)
        {
            stop_activated = false;
        }*/

        if (doorSlideScript.doorsAreMoving)
        {
            Debug.Log("Doors open SOUND");
            if (doorSlideScript.isOpen)
            {
                door_audio = door_opening;
                if (!sound_is_playing)
                {
                    PlaySound(door_opening);
                }

                if (sliding_time >= time_door_opened)
                {
                    StartCoroutine(Wait_DoorSound());
                }
            }
            else if (!doorSlideScript.isOpen)
            {
                Debug.Log("Doors close SOUND");
                door_audio = door_closing;
                if (!sound_is_playing)
                {
                    PlaySound(door_closing); ;
                }
                
                if (sliding_time <= time_door_closed)
                {
                    StartCoroutine(Wait_DoorSound());
                }
            }
        }

    }

    public void PlaySound(AudioSource audio)
    {
        current_audio_source = audio;
        audio.Play();
        sound_is_playing = true;
        if (current_audio_source.name == "moving")
        {
            audio.loop = true;
        }
    }

    public void StopSound(AudioSource audio)
    {
        audio.Stop();
        sound_is_playing = false;
        // current_audio_source = null;
    }

    IEnumerator HandleStopSounds()
    {
        StopSound(moving);
        PlaySound(stop_moving);
        yield return new WaitForSeconds(1.5f);
        StopSound(stop_moving);
    }

    IEnumerator Wait_DoorSound()
    {
        Debug.Log("STOP AUDIO");
        yield return new WaitForSeconds(1f);
        StopSound(door_audio);
    }
}
