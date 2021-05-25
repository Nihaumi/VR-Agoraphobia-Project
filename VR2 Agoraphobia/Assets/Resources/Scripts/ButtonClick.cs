using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public Renderer btnRenderer;

    // Start is called before the first frame update
    void Start()
    {
        btnRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            btnRenderer.material.SetColor("_Color", Color.green);
        }
    }
}
