using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pad;
    public GameObject ball;
    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    public float ballVelocityMult = 1.2f;


    // Start is called before the first frame update
    void Start()
    {
        
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        var obj = Instantiate(ball);
        var b = obj.GetComponent<BallScript>();

        int rand = Random.Range(1, 2);

        if (rand == 1)
            b.direction = new Vector2(2, 0);
        else
            b.direction = new Vector2(-2, 0);
        b.direction *= 1 + ballVelocityMult;


        var padVar1 = Instantiate(pad);
        padVar1.layer = 8;
        var padVar2 = Instantiate(pad);
        padVar2.layer = 9;
        var p1 = padVar1.GetComponent<PadScript>();
        var p2 = padVar2.GetComponent<PadScript>();
        p1.Init(true);
        p2.Init(false);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

}
