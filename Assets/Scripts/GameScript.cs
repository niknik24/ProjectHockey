using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class GameScript : MonoBehaviour
{
    public GameObject ball;
    public int[] score;
    public float duration = 1f;
    bool _isFrozen = false;
    float pend_dur = 0f;
    public float ballVelocityMult = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        score = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        //if (pend_dur < 0 && !_isFrozen)
        //{
          //  StartCoroutine(Freeze(false));
        //}
    }

    public IEnumerator Freeze(bool pad)
    {
        UnityEngine.Debug.Log(score[0]);
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0.3f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        pend_dur = 0;
        _isFrozen = false;
        var obj = Instantiate(ball);
        var b = obj.GetComponent<BallScript>();

        if (pad)
            b.direction += new Vector2(-(40 * 10), 0);
        else
            b.direction += new Vector2((40 * 10), 0);
        b.direction *= 1 + 1 * ballVelocityMult;
    }

    

    public void onGoal(bool pad)
    {
        pend_dur = duration;
        StartCoroutine(Freeze(pad));
        if (pad)
        {
            score[1]++;
        }
        else
        {
            score[0]++;
        }
        UnityEngine.Debug.Log(score[0]);
       
    }
}
