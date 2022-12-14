using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    public static ScorePopup Create(int[] score)
    {
        Transform scorePopupTransform = Instantiate(GameAssets.i.pfScorePopup, Vector3.zero, Quaternion.identity);
        ScorePopup scorePopup = scorePopupTransform.GetComponent<ScorePopup>();
        scorePopup.Setup(score);

        return scorePopup;
    }

    private static int sortingOrder;
    private const float DISAPPEAR_TIMER_MAX = 0.5f;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int[] score)
    {
        textMesh.SetText(score[0] + " : " + score[1]);
        textMesh.fontSize = 36;
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    public void Update()
    {

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } 
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // Start disappearing
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
