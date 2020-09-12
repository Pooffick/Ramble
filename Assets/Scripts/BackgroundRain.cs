using UnityEngine;
using System.Collections;
using System;

public class BackgroundRain : MonoBehaviour {

    public GameObject[] objs;
    public float timeToDie = 15f, spawnRate = 0.5f;
    public Vector3 min, max;
    GameObject player;

    void OnEnable()
    {
        TapNJump.PlayerDead += OnPlayerDead;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            player = gameObject;
        StartCoroutine(spawn());
    }

    void OnPlayerDead(System.Object obj, EventArgs args)
    {
        StopAllCoroutines();
    }

	IEnumerator spawn()
    {
        while (true)
        {
            GameObject go = Instantiate(objs[UnityEngine.Random.Range(0, objs.Length)], player.transform.position + new Vector3(UnityEngine.Random.Range(min.x,max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z)), Quaternion.identity) as GameObject;
            Destroy(go, timeToDie);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        TapNJump.PlayerDead -= OnPlayerDead;
    }
}
