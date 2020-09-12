using UnityEngine;
using System.Collections;
using System;

public class BlockMover : MonoBehaviour {

    public Vector3 from, to;
    public Vector3 angle;
    public float speed = 1;
    [HideInInspector] public float timer = 0;
    int goBack = 1;
    Transform thisTrans;

    public enum BlockType
    {
        horizontal, rotating
    }

    public BlockType type = BlockType.horizontal;

    public static float difficulty = 0.9f;

    void OnEnable()
    {
        GameController.IncreaseDifficult += OnIncreaseDifficult;
        GameController.DecreaseDifficult += OnDecreaseDifficult;
    }

    void Start()
    {
        thisTrans = transform;
        if (type == BlockType.rotating)
        {
            speed *= flipCoin() ? -1 : 1;
        }
    }

    bool flipCoin()
    {
        return System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
    }

    void Update () {
        if (type == BlockType.horizontal)
        {
            if (timer <= 0 && goBack == -1)
                goBack = 1;

            if (timer >= 1 && goBack == 1)
                goBack = -1;

            timer += Time.deltaTime * goBack * speed * difficulty;

            thisTrans.localPosition = Vector3.Lerp(from, to, timer);
        }
        if(type == BlockType.rotating)
        {
            thisTrans.Rotate(angle * speed * difficulty * Time.deltaTime * 100);
        }
	}

    void OnIncreaseDifficult(System.Object obj, EventArgs args)
    {
        if(difficulty < 1.2f)
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