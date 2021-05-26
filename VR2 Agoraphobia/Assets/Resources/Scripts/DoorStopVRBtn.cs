using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStopVRBtn : MonoBehaviour
{
    public Renderer btnRenderer;
    public GameObject indexFinger;
    float dist;
    float touchDist = 0.8f;
    bool touching;
    public bool animationDone;

    //when key down green else grey

    // Start is called before the first frame update
    void Start()
    {
        btnRenderer = GetComponent<Renderer>();
        indexFinger = GameObject.Find("b_r_index3");
        btnRenderer.material.SetColor("_Color", Color.grey);
    }

    // Update is called once per frame
    void Update()
    {
        if (!indexFinger)
        {
            indexFinger = GameObject.Find("b_r_index3");
        }

        if (indexFinger)
        {
            IsTouching();
            if (touching)
            {
                btnRenderer.material.SetColor("_Color", Color.green);
            }
        }

        if (animationDone)
        {
            btnRenderer.material.SetColor("_Color", Color.grey);
        }
    }

    void IsTouching()
    {
        dist = Vector3.Distance(indexFinger.transform.position, transform.position);
        if (dist < touchDist)
        {
            animationDone = false;
            touching = true;
            Debug.Log("distance small: " + dist);
        }
    }
}
