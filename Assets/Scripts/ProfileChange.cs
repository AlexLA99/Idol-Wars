using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileChange : MonoBehaviour
{
    private Button button;
    public Sprite hoomie;
    public Sprite shinobu;
    public Sprite walky;
    public Sprite leafa;
    public Sprite sakura;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        if (PlayerPrefs.GetInt("imageID") == 0)
        {
            button.image.sprite = hoomie;
        }
        else if (PlayerPrefs.GetInt("imageID") == 1)
        {
            button.image.sprite = shinobu;
        }
        else if (PlayerPrefs.GetInt("imageID") == 2)
        {
            button.image.sprite = walky;
        }
        else if (PlayerPrefs.GetInt("imageID") == 3)
        {
            button.image.sprite = leafa;
        }
        else if (PlayerPrefs.GetInt("imageID") == 4)
        {
            button.image.sprite = sakura;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPressed(int number)
    {
        if (number == 0)
        {
            button.image.sprite = hoomie;
        }
        else if (number == 1)
        {
            button.image.sprite = shinobu;
        }
        else if (number == 2)
        {
            button.image.sprite = walky;
        }
        else if (number == 3)
        {
            button.image.sprite = leafa;
        }
        else if (number == 4)
        {
            button.image.sprite = sakura;
        }
        PlayerPrefs.SetInt("imageID", number);
    }
}
