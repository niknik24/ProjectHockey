using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using TMPro;

public class GameScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballcl;
    public GameObject pad;
    public int[] score;
    public float duration = 1f;
    bool _isFrozen = false;
    bool _gameFinished = false;
    float pend_dur = 0f;
    public float ballVelocityMult = 1.2f;
    public GameObject EndGameUI;

    [SerializeField] private TextMeshProUGUI _EndScoreUI;
    [SerializeField] private TextMeshProUGUI _WinnerTextUI;
    [SerializeField] private TextMeshProUGUI _scoreTextUI;
    // Start is called before the first frame update
    void Start()
    {
        score = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gameFinished)
        {
            Restart();
        }

        if (pend_dur < 0 && !_isFrozen)
        {
            StartCoroutine(Freeze(false));
        }


        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightControl)) {

            ballcl = GameObject.Find("Ball(Clone)");
            Destroy(ballcl);

            var obj = Instantiate(ball);
            var b = obj.GetComponent<BallScript>();

            int rand = Random.Range(1, 2);

            if (rand == 1)
                b.direction = new Vector2(2, 0);
            else
                b.direction = new Vector2(-2, 0);
            b.direction *= 1 + ballVelocityMult;
        }
    }

    public IEnumerator Freeze(bool pad)
    {
        _scoreTextUI.text = string.Empty;
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
            b.direction = new Vector2(-2, 0);
        else
            b.direction = new Vector2(2, 0);
        b.direction *= 1 + ballVelocityMult;
        _scoreTextUI.text = $"{score[0]} : {score[1]}";
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
        if (score[0] == 3 || score[1] == 3)
        {
            _WinnerTextUI.text = (score[0] == 3) ? "Left player win" : "Right player win";
            _EndScoreUI.text = $"{score[0]} : {score[1]}";
            EndGameUI.SetActive(true);
            Time.timeScale = 0f;
            foreach(GameObject p in GameObject.FindGameObjectsWithTag("Pad"))
                Destroy(p);
            _gameFinished = true;
            return;
        }

        ScorePopup.Create(score);
    }

    public void Restart()
    {
        _gameFinished = false;
        _scoreTextUI.text = string.Empty;
        score[0] = 0;
        score[1] = 0;
        EndGameUI.SetActive(false);
        var padVar1 = Instantiate(pad);
        padVar1.layer = 8;
        var padVar2 = Instantiate(pad);
        padVar2.layer = 9;
        var p1 = padVar1.GetComponent<PadScript>();
        var p2 = padVar2.GetComponent<PadScript>();
        p1.Init(true);
        p2.Init(false);
    }
}
