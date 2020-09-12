using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    Transform playerTransform;
    public Color[] PlayerAndRain, Walls, FogAndCameraAndRotor, Enemies;
    public Material Player, Stand, Fog, Enemy;
    public GameObject snow, sun;
    BackgroundRain[] rains;

    public GameObject tap;

    static float timer = 180;

    public Text scoreText, bestScoreText;
    int score = 0;
    int rand;

    public static EventHandler IncreaseDifficult, DecreaseDifficult;

    public GameObject GameMenu, GameOverMenu;

    bool gameOver = false;

    static bool firstEnter = false;

    bool _Slowed, _SlowCoroutine;
    float _SlowingFactor, _SlowValue = 0.1f, _SmoothRate = 0.01f;

    void Awake()
    {
        snow.SetActive(false);
        sun.SetActive(false);
        rains = GetComponents<BackgroundRain>();
        foreach (BackgroundRain rain in rains)
            rain.enabled = false;

        rand = PlayerPrefs.GetInt("Level", 0);

        if (PlayerPrefs.GetInt("Background", 0) == 1)
            EnableBackGround();

        Player.color = PlayerAndRain[rand];
        Stand.color = Walls[rand];
        Fog.color = FogAndCameraAndRotor[rand];
        Enemy.color = Enemies[rand];
        Camera.main.backgroundColor = FogAndCameraAndRotor[rand];
        RenderSettings.fogColor = FogAndCameraAndRotor[rand];

        gameOver = false;
        StopSlowMotion();
    }

    void OnEnable()
    {
        TapNJump.IncreaseScore += OnScoreIncrease;
        TapNJump.PlayerDead += OnPlayerDead;
    }

    void Start()
    {
        if (firstEnter)
        {
            tap.SetActive(false);
        }
        else
        {
            tap.SetActive(true);
        }

        scoreText.text = "Score: " + score;
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        GameMenu.SetActive(true);
        GameOverMenu.SetActive(false);
    }

    void EnableBackGround()
    {

        switch (rand)
        {
            case 0:
                rains[0].enabled = true;
                rains[1].enabled = true;
                break;
            case 1:
                rains[5].enabled = true;
                break;
            case 2:
                snow.SetActive(true);
                break;
            case 3:
                rains[2].enabled = true;
                break;
            case 4:
                rains[0].enabled = true;
                rains[1].enabled = true;
                break;
            case 5:
                rains[0].enabled = true;
                rains[1].enabled = true;
                break;
            case 6:
                rains[0].enabled = true;
                rains[1].enabled = true;
                break;
            case 7:
                rains[3].enabled = true;
                rains[4].enabled = true;
                break;
            case 8:
                rains[6].enabled = true;
                break;
            case 9:
                rains[0].enabled = true;
                rains[1].enabled = true;
                sun.SetActive(true);
                break;
            case 10:
                rains[0].enabled = true;
                rains[1].enabled = true;
                break;
        }
    }

    void Update()
    {

        if (timer > 0)
            timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && GameOverMenu.activeSelf)
        {
            MenuClick();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && tap.activeSelf)
        {
            tap.SetActive(false);
            firstEnter = true;
        }

        if (!gameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            gameOver = true;
            playerTransform.GetComponent<TapNJump>().Death();
            GameOver();
            SlowMotion();
            Invoke("SlowMotion", 0.3f);
        }

        if (gameOver && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Escape)))
        {
            GameOver();
            StopSlowMotion();
        }
    }

    void OnScoreIncrease(System.Object obj, EventArgs args)
    {
        score++;
        scoreText.text = "Score: " + score;

        if (score % 5 == 0)
            if (IncreaseDifficult != null) IncreaseDifficult(null, null);
    }

    void OnPlayerDead(System.Object obj, EventArgs args)
    {
        if (!gameOver)
        {
            gameOver = true;
            if (PlayerPrefs.GetInt("BestScore", 0) < score)
            {
                PlayerPrefs.SetInt("BestScore", score);
                bestScoreText.text = "Best Score: " + score;
                //play newbestscore
            }
            if (DecreaseDifficult != null) DecreaseDifficult(null, null);
            Invoke("GameOver", 0.3f);
            SlowMotion();
            Invoke("SlowMotion", 0.5f);
        }
    }

    void StopSlowMotion()
    {
        if (!_SlowCoroutine && Time.timeScale < 1.0f)
        {
            _SlowCoroutine = true;
            _SlowingFactor = 0.0f;
            StartCoroutine(Slowing());
        }
    }

    void OnDisable()
    {
        TapNJump.IncreaseScore -= OnScoreIncrease;
        TapNJump.PlayerDead -= OnPlayerDead;
    }

    void GameOver()
    {
        playerTransform.GetComponent<TapNJump>().dead = true;
        GameOverMenu.SetActive(true);
        GameMenu.SetActive(false);
        if (timer <= 0)
        {
            timer = 180;
        }
    }

    public void RestartClick()
    {
        GameOverMenu.SetActive(false);
        GameMenu.SetActive(false);
        Application.LoadLevelAsync(Application.loadedLevel);
    }
    public void MenuClick()
    {
        GameOverMenu.SetActive(false);
        GameMenu.SetActive(false);
        Application.LoadLevelAsync("Menu");
    }

    /// <summary> 
    /// Метод замедления времени. 
    /// </summary> 
    public void SlowMotion()
    {
        _Slowed = !_Slowed;
        if (!_SlowCoroutine)
        {
            _SlowCoroutine = true;
            StartCoroutine(Slowing());
        }
    }

    /// <summary> 
    /// Метод изменения значения кадра. 
    /// </summary> 
    void FrameChangeValue()
    {
        Time.timeScale = _SlowingFactor;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    /// <summary> 
    /// Сопрограмма плавного замедления времени. 
    /// </summary> 
    IEnumerator Slowing()
    {

        while (_Slowed && _SlowingFactor != _SlowValue)
        {
            if (_SlowingFactor > _SlowValue)
            {
                _SlowingFactor -= _SmoothRate;
            }
            else
            if (_SlowingFactor < _SlowValue)
            {
                _SlowingFactor = _SlowValue;
            }
            FrameChangeValue();
            yield return null;
        }

        while (!_Slowed && _SlowingFactor != 1.0f)
        {
            if (_SlowingFactor < 1.0f)
            {
                _SlowingFactor += _SmoothRate;
            }
            else
            if (_SlowingFactor > 1.0f)
            {
                _SlowingFactor = 1.0f;
            }
            FrameChangeValue();
            yield return null;
        }

        _SlowCoroutine = false;
        yield return null;
    }
}