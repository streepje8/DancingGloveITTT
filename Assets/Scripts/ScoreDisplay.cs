using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public Material mat;
    private TMP_Text text;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    public void DisplayScore(string score, Color color)
    {
        text.text = score;
        mat.SetColor("_FaceColor", color);
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 7));
        delay = 0.5f;
    }

    float delay = 0f;

    void Update()
    {
        if (delay <= 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 10f * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 10f * Time.deltaTime);
        } else
        {
            delay -= Time.deltaTime;
        }
    }
}
