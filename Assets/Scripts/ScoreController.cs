using System;
using System.Collections;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    protected static int time;
    protected static int hScore;
    protected static Score asteroids;
    protected static int cScore;

    protected static int CScore
    {
        get => cScore;
        private set
        {
            cScore = value;
            OnScoreChange?.Invoke(cScore);
        }
    }

    private void Start()
    {
        //initial configuration
        StartCoroutine(CountScore());
        CScore = 0;
        time = 0;
        hScore = PlayerPrefs.GetInt("hScore");
        asteroids = new Score("0");
        Settings.isNewRecord = false;
    }

    private void Update()
    {
        //rewrite highscore if necessary
        if (hScore < CScore)
        {
            hScore = CScore;
            Settings.isNewRecord = true;
            PlayerPrefs.SetInt("hScore", hScore);
        }

        //increasing complexity
        Settings.speed = Mathf.Log10(CScore + 10) * 150;
        Settings.speedSpawn = 2 / Mathf.Log10(CScore + 10);
    }

    public static event Action<int> OnScoreChange;

    private IEnumerator CountScore()
    {
        //update counters every second
        yield return new WaitForSeconds(1);
        CScore++;
        if (Settings.isBoost) CScore++;
        time++;
        PlayerPrefs.SetInt("allTime", PlayerPrefs.GetInt("allTime") + 1);
        StartCoroutine(CountScore());
    }
}