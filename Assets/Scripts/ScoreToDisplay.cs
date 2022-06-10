using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreToDisplay : Singleton<ScoreToDisplay>
{
    public ScoreDisplay scoreDisplay;

    public List<string> grades = new List<string>();
    [ColorUsage(true, true)]
    public List<Color> colors = new List<Color>();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TranslateScore(int score)
    {
        int MAPPED = score;
        scoreDisplay.DisplayScore(grades[MAPPED], colors[MAPPED]);
    }
}
