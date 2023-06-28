using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public Animator anim;

    public KeyCode left, right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(left))
        {
            anim.SetTrigger("attackLeft");
        }
        if (Input.GetKeyDown(right))
        {
            anim.SetTrigger("attackRight");
        }
    }
}
