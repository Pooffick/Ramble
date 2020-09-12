using UnityEngine;
using System.Collections;

public class EnemyBuilder : MonoBehaviour {

    public GameObject[] enemies;
    public float lenght = 5;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position + Vector3.up * lenght, transform.rotation);
        }
    }
}
