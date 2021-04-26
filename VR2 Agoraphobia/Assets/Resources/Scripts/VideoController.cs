using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;

public class VideoController : MonoBehaviour
{

    public VideoPlayer video;
    public bool isPlaying = false;


    private void Start()
    {
        video.playOnAwake = false;
        video.Play();
        isPlaying = true;
    }

    private void Update()
    {
        //pause/play the video
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isPlaying == true)
            {
                video.Pause();
                isPlaying = false;                
                Debug.Log("pause");
                return;
            }
            else if (isPlaying == false)
            {
                video.Play();
                isPlaying = true;
                Debug.Log("play");
                return;
            }

        }
        //speed up/ down
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            video.playbackSpeed += 0.1f;
            Debug.Log("faster");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            video.playbackSpeed -= 0.1f;
            Debug.Log("slower");
        }

        //restart
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            Rewind();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FastForward();
        }
    }

    void Rewind()
    {
        video.frame = 0;
        Debug.Log("rewind");
    }

    void FastForward()
    {
        video.frame += 5*60;
        Debug.Log("skip");
    }

}
