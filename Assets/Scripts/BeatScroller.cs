using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    // Start is called before the first frame update
    public float beatTempo;

    public bool hasStarted;
    public bool paused;

    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted || paused)
        {
            //if(Input.anyKeyDown)
            //{
            //    hasStarted = true;
            //}
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
