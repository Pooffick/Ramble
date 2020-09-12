using UnityEngine;
using System.Collections;
using System;

public class AnotherBlockMover : MonoBehaviour {

    public Vector3 from, to, moveTo;
    Vector3 normal;

    public static float difficulty = 0.9f;
    Transform thisTrans;
    float waitTimer, goTimer, goBackTimer;
    public float timeWait = 2f, timeGo = 0.1f, timeGoBack = 1;

    public enum State
    {
        idle, moving, movingBack
    }

    public State currentState = State.idle;

    void OnEnable()
    {
        GameController.IncreaseDifficult += OnIncreaseDifficult;
        GameController.DecreaseDifficult += OnDecreaseDifficult;
    }

    void Start()
    {
        thisTrans = transform;
        normal = thisTrans.localPosition;
        goBackTimer = 0.7f;
    }

    void Update()
    {
        if (currentState == State.idle)
        {
            if (waitTimer > 0)
                waitTimer -= Time.deltaTime * difficulty;
            else
            {
                currentState = State.moving;
                goTimer = 0;
            }
        }
        else if (currentState == State.moving)
        {
            if (goTimer < 1)
                goTimer += Time.deltaTime / timeGo;
            else
            {
                currentState = State.movingBack;
                goBackTimer = 0;
            }
        }
        else if (currentState == State.movingBack)
        {
            if (goBackTimer < 1)
                goBackTimer += Time.deltaTime / timeGoBack;
            else
            {
                currentState = State.idle;
                waitTimer = timeWait;
            }
        }

        switch (currentState)
        {
            case State.idle:
                transform.localPosition = Vector3.Lerp(from, new Vector3(UnityEngine.Random.Range(-to.x, to.x), UnityEngine.Random.Range(-to.y, to.y), UnityEngine.Random.Range(-to.z, to.z)), Time.deltaTime);
                break;

            case State.moving:
                transform.localPosition = Vector3.Lerp(normal, moveTo, goTimer); 
                break;

            case State.movingBack:
                transform.localPosition = Vector3.Lerp(moveTo, normal, goBackTimer);
                break;
        }
    }

    void OnIncreaseDifficult(System.Object obj, EventArgs args)
    {
        if (difficulty < 1.2f)
            difficulty += 0.025f;
    }

    void OnDecreaseDifficult(System.Object obj, EventArgs args)
    {
        difficulty = 0.9f;
    }

    void OnDisable()
    {
        GameController.IncreaseDifficult -= OnIncreaseDifficult;
        GameController.DecreaseDifficult -= OnDecreaseDifficult;
    }
}
