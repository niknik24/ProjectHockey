using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    float speed = 8;
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
        if (rb.isKinematic)
          if (Input.GetButtonDown("Fire1"))
        {
           rb.isKinematic = false;
          rb.AddForce(direction);
        }

        if (transform.position.y < GameManager.bottomLeft.y + radius && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }

        if (transform.position.y > GameManager.topRight.y - radius && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }

        if (transform.position.x < GameManager.bottomLeft.x + radius && rb.velocity.x < 0)
        {
            Destroy(gameObject);
            game.GetComponent<GameScript>().onGoal(true);
        }


        if (transform.position.x > GameManager.topRight.x - radius && rb.velocity.x > 0)
        {
            Destroy(gameObject);
            game.GetComponent<GameScript>().onGoal(false);
        }
    }

}
