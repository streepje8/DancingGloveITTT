using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class MoveManager : Singleton<MoveManager>
{
    //91700
    public bool recordmode = false;
    public TMP_Text scoreText;
    public int gamescore = 0;
    public VideoPlayer player;
    public ArduinoInput arduino;
    public TextAsset leveldata;
    public List<DanceMove> moves = new List<DanceMove>();
    public float leveltime = 0f;
    public Rotator rotator;

    private bool inmove = false;
    private float timerboi = 0f;
    private float startTime = 0f;
    private DanceMove currentmove;
    private int smoothScore = 0;
    private void Awake()
    {
        Instance = this;
        if(!recordmode)
        {
            moves = JsonConvert.DeserializeObject<List<DanceMove>>(leveldata.text);
        }
        moves.Sort(delegate(DanceMove a, DanceMove b)
        {
            if (a.startTime == b.startTime) return 0;
            if (a.startTime > b.startTime) return 1;
            if (b.startTime > a.startTime) return -1;
            return 0;
        });
    }


    private float boi = 0f;
    private void Update()
    {
        boi += Time.deltaTime;
        //smoothScore = Mathf.FloorToInt(Mathf.Lerp(smoothScore, gamescore, 10f * Time.deltaTime));
        if (boi > 0.01f)
        {
            if (smoothScore + 100 <= gamescore)
            {
                smoothScore += 100;
            }
            if (smoothScore + 10 <= gamescore)
            {
                smoothScore += 10;
            }
            boi = 0;
        }
        scoreText.text = smoothScore.ToString();
        leveltime = (float)player.time;
        if(recordmode)
        {
            if(Input.GetMouseButtonDown(0) && !inmove)
            {
                inmove = true;
                timerboi = 0f;
                rotator.ResetRot();
                startTime = leveltime;
            }
            if(inmove)
            {
                timerboi += Time.deltaTime;
                if(timerboi >= 0.5f)
                {
                    moves.Add(new DanceMove(arduino.transform.rotation * Vector3.up, startTime));
                    inmove = false;
                    ScoreToDisplay.Instance.TranslateScore(1);
                }
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(JsonConvert.SerializeObject(moves, Formatting.Indented));
            }
        } else
        {
            if (moves.Count > 0)
            {
                if (leveltime >= moves[0].startTime)
                {
                    inmove = true;
                    rotator.ResetRot();
                    timerboi = 0f;
                    currentmove = moves[0];
                    moves.Remove(moves[0]);
                }
                if (inmove)
                {
                    timerboi += Time.deltaTime;
                    if (timerboi >= 0.5f)
                    {
                        Vector3 userinput = arduino.transform.rotation * Vector3.up;
                        float score = 1 - Vector3.Distance(userinput, new Vector3(currentmove.x, currentmove.y, currentmove.z));
                        int endscore = 4;
                        if (score > 0.4)
                        {
                            gamescore += 100;
                            endscore = 3;
                        }
                        if (score > 0.6)
                        {
                            gamescore += 500;
                            endscore = 2;
                        }
                        if (score > 0.7)
                        {
                            gamescore += 1000;
                            endscore = 1;
                        }
                        /*
                        if (score > 0.95)
                            endscore = 0; */
                        ScoreToDisplay.Instance.TranslateScore(endscore);
                        inmove = false;
                    }
                }
            }
        }
    }
}

[System.Serializable]
public struct DanceMove
{
    public float x;
    public float y;
    public float z;

    public float startTime;
    public DanceMove(Vector3 endpoint, float time)
    {
        this.x = endpoint.x;
        this.y = endpoint.y;
        this.z = endpoint.z;
        this.startTime = time;
    }
}