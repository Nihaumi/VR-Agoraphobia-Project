using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFinger : MonoBehaviour
{
    public GameObject indexFinger_R;
    public GameObject indexFinger_L;

    // Start is called before the first frame update
    void Start()
    {
        FindFingers();
    }

    // Update is called once per frame
    void Update()
    {
        FindFingers();
    }
    void FindFingers()
    {
        if (indexFinger_L == null || indexFinger_R == null)
        {
            indexFinger_R = GameObject.Find("b_r_index3");
            Debug.Assert(indexFinger_R != null, "no index R");
            indexFinger_L = GameObject.Find("b_l_index3");
            Debug.Assert(indexFinger_L != null, "ni index L");
        }
    }
}
