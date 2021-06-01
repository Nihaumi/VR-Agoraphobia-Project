using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color color_btnInactive;
    public Color color_btnStopinactive;
    public Color color_btnStopactive;
    public Color color_btnActive;


    // Start is called before the first frame update
    void Start()
    {
        color_btnInactive = new Color32(255, 240, 237, 255);
        color_btnActive = new Color32(95, 241, 52, 255);
        color_btnStopinactive = new Color32(219, 63, 59, 255);
        color_btnStopactive = new Color32(255, 7, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
