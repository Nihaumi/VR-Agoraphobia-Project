using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource door_opening;
    public AudioSource door_closing;
    public AudioSource start_moving;
    public AudioSource stop_moving;
    public AudioSource moving;

    public bool sound_is_playing;
    public AudioSource current_audio_source;
    public float fade_time;
    public float start_door_opening;
    public float start_door_closing;
    // Start is called before the first frame update
    void Start()
    {
        start_door_opening = 0.05f;
        start_door_closing = 0.95f;
        fade_time = 0.5f;
        sound_is_playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioSource audio)
    {
        current_audio_source = audio;
        audio.Play();
        sound_is_playing = true;
    }

    public void StopSound(AudioSource audio)
    {
        /* Debug.Log("selected audio: " + audio);

         float start_volume = audio.volume;
         Debug.Log("STRAT audio volume = " + start_volume);
         while(audio.volume > 0)
         {
             audio.volume = Mathf.Lerp(start_volume, 0, fade_time*Time.deltaTime);
             Debug.Log("CURRENT audio volume = " + audio.volume);
         }
         if(audio.volume <= 0)
         {
             audio.Stop();
             audio.volume = start_volume;
             sound_is_playing = false;
         }*/
        audio.Stop();
        sound_is_playing = false;

    }
}
