using System;
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

    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        width = transform.localScale.x;
        game = GameObject.FindGameObjectWithTag("Script");
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    void FixedUpdate()
    {
        float move = UnityEngine.Input.GetAxis(inpt) * Time.fixedDeltaTime * speed;
        float move2 = UnityEngine.Input.GetAxis(inpt2) * Time.fixedDeltaTime * speed;
        
        

        //transform.Translate(move * Vector2.left);
        _rigidbody2D.MovePosition(transform.position + new Vector3(-move,move2,0));
        //_rigidbody2D.AddForce(new Vector2(10*Math.Sign(-move), 10*Math.Sign(move2)));
        //transform.Translate(move2 * Vector2.up);
    }

}
