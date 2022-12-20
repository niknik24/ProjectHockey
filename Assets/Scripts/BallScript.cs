using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BallScript : MonoBehaviour
{

    // float speed = 8;
    float radius;
    public Vector2 direction;
    GameObject game;
    Rigidbody2D rb;
    float deltaX;

    // Start is called before the first frame update
    void Start()
    {
        radius = transform.localScale.x / 2;
        game = GameObject.FindGameObjectWithTag("Script");
        rb = GetComponent<Rigidbody2D>();
        deltaX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic && Input.GetButtonDown("Fire1"))
        {
          rb.isKinematic = false;
          rb.AddForce(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("KillWall"))
        {
            Destroy(gameObject);
            game.GetComponent<GameScript>().onGoal(collision.gameObject.tag.Contains("Left"));
        }
    }
}
