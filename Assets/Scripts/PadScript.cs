using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PadScript : MonoBehaviour
{
    
    float speed = 8;
    float height;
    float width;
    string inpt;
    string inpt2;
    public bool isRightPad;
    GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        width = transform.localScale.x;
        game = GameObject.FindGameObjectWithTag("Script");
    }

    public void Init(bool isRight)
    {
        isRightPad = isRight;
        Vector2 pos = Vector2.zero;

        if (isRight)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;
            inpt = "PadRight";
            inpt2 = "PadRightUp";
        }
        else
        {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;
            inpt = "PadLeft";
            inpt2 = "PadLeftUp";
        }

        transform.position = pos;
        transform.name = inpt;
    }

    // Update is called once per frame
    void Update()
    {
        float move = UnityEngine.Input.GetAxis(inpt) * Time.deltaTime * speed;
        float move2 = UnityEngine.Input.GetAxis(inpt2) * Time.deltaTime * speed;

        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move2 < 0)
        {
            move2 = 0;
        }

        if (transform.position.x < GameManager.bottomLeft.x + width / 2 && move > 0)
        {
            move = 0;
        }

        if (transform.position.y > GameManager.topRight.y - height / 2 && move2 > 0)
        {
            move2 = 0;
        }

        if (transform.position.x > GameManager.topRight.x - width / 2 && move < 0)
        {
            move = 0;
        }

        if (inpt.Equals("PadRight"))
        {
            if (transform.position.x < 0 + width && move > 0)
                move = 0;
        }
        if (inpt.Equals("PadLeft"))
        {
           if (transform.position.x > 0 - width && move < 0)
               move = 0;
        }

        transform.Translate(move * Vector2.left);
        transform.Translate(move2 * Vector2.up);
    }

}
