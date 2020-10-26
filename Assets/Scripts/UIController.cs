using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : ScoreController
{
    [SerializeField] private AudioClip buttonClip;
    
    public RectTransform text;

    public GameObject pauseMenuPanel;
    public GameObject whileGamePanel;
    public GameObject gameOverPanel;
    public GameObject newRecordText;

    public Text textCScore;
    public Text textTime;
    public Text textHScore;
    public Text textAsteroids;
    public Text textAllAsteroids;
    public Text textAllTime;
    public Text textHScore2;

    private void Awake()
    {
        OnScoreChange += i => { Debug.Log("Score was changed"); };
    }

    private void Start()
    {
        //initial configuration
        Time.timeScale = 0;
        gameOverPanel.SetActive(false);
        newRecordText.SetActive(false);
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!Settings.isGameOver)
        {
            //pause key control
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Mathf.Approximately(1f,Time.timeScale))
                {
                    Time.timeScale = 0;
                    AudioController.soundAudioSource.PlayOneShot(buttonClip, AudioController.ValueVolumeSound);
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
            else if (Input.anyKeyDown) Time.timeScale = 1;
        }
        else
        {
            //filling in fields with information
            textHScore2.text = $"HIGH SCORE: {PlayerPrefs.GetInt("hScore")}";
            textAllAsteroids.text = $"ALL ASTEROIDS: {PlayerPrefs.GetInt("allAsteroids")}";
            textAllTime.text =
                $"ALL TIME: {PlayerPrefs.GetInt("allTime") / 60} min {PlayerPrefs.GetInt("allTime") % 60} sec";

            //control keys in game over
            if (Input.GetKeyDown(KeyCode.E)) Application.Quit();
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
                Settings.isGameOver = false;
                gameOverPanel.SetActive(false);
            }
        }

        //animation of increasing and decreasing text
        var scale = Mathf.PingPong(Time.unscaledTime / 2, 0.3f) + 0.8f;
        text.localScale = new Vector3(scale, scale, 0);

        //UI control (enabling and disabling)
        if (Time.timeScale.Equals(0))
        {
            if (!Settings.isGameOver) pauseMenuPanel.SetActive(true);
            else gameOverPanel.SetActive(true);
            whileGamePanel.SetActive(false);
        }
        else
        {
            if (!Settings.isGameOver) pauseMenuPanel.SetActive(false);
            whileGamePanel.SetActive(true);
        }

        if (Settings.isNewRecord) newRecordText.SetActive(true);


        //filling in fields with information
        textCScore.text = $"SCORE: {CScore.ConvertScore(1000)}";
        textTime.text = $"TIME: {time} sec";
        if (!asteroids.ToString().Equals(textAsteroids.text) && asteroids.ToString() != null)
            textAsteroids.text = $"ASTEROIDS: {TestReflection(asteroids)}";
        textHScore.text = $"HIGH SCORE: {hScore}";
    }

    private static int TestReflection(Score s)
    {
        var fi = typeof(Score).GetField("_value", BindingFlags.Instance | BindingFlags.NonPublic);
        var result = (int) fi.GetValue(s);
        return result;
    }
    
}