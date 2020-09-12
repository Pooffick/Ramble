using UnityEngine;
using System.Collections;
using System;

public class LazerActivator : MonoBehaviour {

    GameObject lazer;
    public float wTime = 1, cdTime = 1;
    float timerCD = 0, timerW;
    bool isWorking = true;

    public static float difficulty = 0.9f;

    void OnEnable()
    {
        GameController.IncreaseDifficult += OnIncreaseDifficult;
        GameController.DecreaseDifficult += OnDecreaseDifficult;
    }

    void Start()
    {
        timerW = wTime;
        lazer = transform.GetChild(0).gameObject;
    }

    void OnIncreaseDifficult(System.Object obj, EventArgs args)
    {
        if (difficulty < 1.15f)
            difficulty += 0.02f;
    }

    void OnDecreaseDifficult(System.Object obj, EventArgs args)
    {
        difficulty = 0.9f;
    }

    void Update ()
    {
        if (isWorking)
        {
            if (timerW > 0)
                timerW -= Time.deltaTime;

            if (!lazer.activeSelf)
                lazer.SetActive(true);

            if (timerW <= 0)
            {
                timerCD = cdTime/difficulty;
                isWorking = false;
            }
        }
        else
        {
            if (timerCD > 0)
                timerCD -= Time.deltaTime;

            if (lazer.activeSelf)
                lazer.SetActive(false);

            if (timerCD <= 0)
            {
                timerW = wTime;
                isWorking = true;
            }
        }
	}

    void OnDisable()
    {
        GameController.IncreaseDifficult -= OnIncreaseDifficult;
        GameController.DecreaseDifficult -= OnDecreaseDifficult;
    }
}
