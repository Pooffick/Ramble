using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MenuController : MonoBehaviour {

    public GameObject Menu;

    public Color[] PlayerAndRain, FogAndCameraAndRotor;
    BackgroundRain[] rains;
    public Material Player, Fog;
    int rand;

    public GameObject pButton, sPannel, sButton, shButton, shPanel;

    static int count;
    static bool isOn;
    public Text txt;

    bool _Slowed, _SlowCoroutine;
    float _SlowingFactor, _SlowValue = 0.1f, _SmoothRate = 0.01f;

    void Awake()
    {
        count = PlayerPrefs.GetInt("Level", 0);
        isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("Background", 0));
        if (isOn)
            txt.text = "Use Background";
        else
            txt.text = "Don't Use Background";
        ChangeColor();
        StopSlowMotion();
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

    void ChangeColor()
    {
        rand = PlayerPrefs.GetInt("Level", 0);
        Player.color = PlayerAndRain[rand];
        Fog.color = FogAndCameraAndRotor[rand];
        Camera.main.backgroundColor = FogAndCameraAndRotor[rand];
        RenderSettings.fogColor = FogAndCameraAndRotor[rand];
    }

    void Start () {
        sPannel.SetActive(false);
        pButton.SetActive(true);
        sButton.SetActive(true);
        shButton.SetActive(true);
        shPanel.SetActive(false);
        rains = Camera.main.GetComponents<BackgroundRain>();
        foreach (BackgroundRain r in rains)
            r.enabled = isOn;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnToggleChange()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt("Background", System.Convert.ToInt32(isOn));
        foreach (BackgroundRain r in rains)
            r.enabled = isOn;

        if (isOn)
            txt.text = "Use Background";
        else
            txt.text = "Don't Use Background";
    }

    public void OnSelectorChanges()
    {
        if (count < 10)
            count++;
        else
            count = 0;

        PlayerPrefs.SetInt("Level", count);
        ChangeColor();
    }

    public void PlayClick()
    {
        Menu.SetActive(false);
        Application.LoadLevelAsync("Game");
    }

    public void QuitClick()
    {
        Menu.SetActive(false);
        Application.Quit();
    }

    public void AdsClick()
    {
        //buy 
    }

    public void ShowSettings()
    {
        pButton.SetActive(false);
        sButton.SetActive(false);
        shButton.SetActive(false);
        sPannel.SetActive(true);
    }

    public void ShowShare()
    {
        pButton.SetActive(false);
        sButton.SetActive(false);
        shButton.SetActive(false);
        shPanel.SetActive(true);
    }

    public void HideShare()
    {
        shPanel.SetActive(false);
        shButton.SetActive(true);
        pButton.SetActive(true);
        sButton.SetActive(true);
    }

    public void HideSettings()
    {
        sPannel.SetActive(false);
        shButton.SetActive(true);
        pButton.SetActive(true);
        sButton.SetActive(true);
    }

    public void OpenBrowser()
    {
        Application.OpenURL("market://details?id=mobi.sibstone.ramble");
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
        //_SoundSource.pitch = _SlowingFactor;
        //_MusicSource.pitch = Mathf.Clamp(_SlowingFactor + 0.4f, -1.0f, 1.0f);
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
