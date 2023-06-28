using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject badEffect, goodEffect, greatEffect, perfectEffect, missEffect;

    private bool rendering = false;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.position.y <= 5.5 && !rendering)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            rendering = true;
        }

        if(Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                if (GameManager.instance.first)
                {
                    GameManager.instance.first = false;
                }
                else
                {
                    Destroy(FindObjectOfType<EffectObject>().gameObject);
                }


                //GameManager.instance.NoteHit();

                if(GameManager.instance.shinobu && GameManager.instance.ultimateWorking && Mathf.Abs(transform.position.y) <= 0.45f)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, new Vector3(0f, -0.89f, 0f), perfectEffect.transform.rotation);
                }
                else if(GameManager.instance.leafa && GameManager.instance.abilityWorking && Mathf.Abs(transform.position.y) > 0.20f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, new Vector3(0f, -0.89f, 0f), goodEffect.transform.rotation);
                }
                else if (GameManager.instance.hoomie && GameManager.instance.ultimateWorking && Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Great");
                    GameManager.instance.GreatHit();
                    Instantiate(greatEffect, new Vector3(0f, -0.89f, 0f), greatEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.45)
                {
                    Debug.Log("Bad");
                    GameManager.instance.BadHit();
                    Instantiate(badEffect, new Vector3(0f, -0.89f, 0f), badEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.20f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, new Vector3(0f, -0.89f, 0f), goodEffect.transform.rotation);
                }
                else if (GameManager.instance.walky && GameManager.instance.abilityWorking && Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, new Vector3(0f, -0.89f, 0f), perfectEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Great");
                    GameManager.instance.GreatHit();
                    Instantiate(greatEffect, new Vector3 (0f, -0.89f, 0f), greatEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, new Vector3(0f, -0.89f, 0f), perfectEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            if (gameObject.activeInHierarchy)
            {
                if (GameManager.instance.first)
                {
                    GameManager.instance.first = false;
                }
                else
                {
                    Destroy(FindObjectOfType<EffectObject>().gameObject);
                }
                if (GameManager.instance.hoomie && GameManager.instance.ultimateWorking)
                {
                    Debug.Log("Great");
                    GameManager.instance.GreatHit();
                    Instantiate(greatEffect, new Vector3(0f, -0.89f, 0f), greatEffect.transform.rotation);
                }
                else
                {
                    GameManager.instance.NoteMissed();
                    Instantiate(missEffect, new Vector3(0f, -0.89f, 0f), missEffect.transform.rotation);
                }
            }
        }
    }
}
