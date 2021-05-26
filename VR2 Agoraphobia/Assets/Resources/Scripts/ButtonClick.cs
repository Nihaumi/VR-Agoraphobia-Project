using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public Renderer btnRenderer;
    public GameObject cube;
    float dist;
    float touchDist = 0.8f;
    bool touching;

    //when key down green else grey

    // Start is called before the first frame update
    void Start()
    {
        btnRenderer = GetComponent<Renderer>();
        btnRenderer.material.SetColor("_Color", Color.grey);
        cube = GameObject.Find("touch");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(cube.transform.position, transform.position);
        IsTouching();
    }

    void IsTouching()
    {
        if (dist < touchDist)
        {
            touching = true;
            btnRenderer.material.SetColor("_Color", Color.green);
            Debug.Log("distance small: " + dist);
        }
        if (dist > touchDist)
        {
            btnRenderer.material.SetColor("_Color", Color.grey);
            touching = false;
        }
    }

}
