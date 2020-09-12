using UnityEngine;
using System.Collections;
using System;

public class TapNJump : MonoBehaviour {

    public static EventHandler IncreaseScore, PlayerDead;

    public float Force = 3;
    AudioSource thisAuds, deathAuds;
    public GameObject mp;
    Transform thisTrans;
    Rigidbody thisRbody;
    [HideInInspector]public bool dead = false;

    public Vector3 scaleTo;

    public enum playerStates
    {
        alive, dead
    }

    public static playerStates currentState;

    void Start () {
        thisTrans = transform;
        thisAuds = GetComponent<AudioSource>();
        deathAuds = transform.GetChild(4).GetComponent<AudioSource>();
        thisRbody = GetComponent<Rigidbody>();
        currentState = playerStates.alive;
    }

  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentState != playerStates.dead && thisRbody.velocity.magnitude < 20 && !dead)
        {
            thisRbody.AddForce(Vector3.up * Force * 100);
            thisTrans.localScale = scaleTo;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            thisTrans.localScale = Vector3.one;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.CompareTag("Enemy"))
        {
            Invoke("Death", 0.2f);
            mp.SetActive(false);
            deathAuds.Play();
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Score"))
        {
            if (IncreaseScore != null) IncreaseScore(null, null);
            Destroy(coll.gameObject);
            thisAuds.Play();
        }

        if (coll.transform.CompareTag("Lazer"))
        {
            Invoke("LazerDeath", 0.1f);
            mp.SetActive(false);
            deathAuds.Play();
        }
        if (coll.transform.CompareTag("Flap"))
        {
            flapDeath();
            mp.SetActive(false);
            deathAuds.Play();
        }
    }

    public void Death()
    {
        if (!dead)
        {
            dead = true;
            thisTrans.GetChild(3).gameObject.SetActive(true);
            thisTrans.GetChild(3).GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            thisTrans.GetChild(3).SetParent(null);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<MeshRenderer>().enabled = false;
            Die();
        }
    }

    void LazerDeath()
    {
        if (!dead)
        {
            dead = true;
            thisTrans.GetChild(0).gameObject.SetActive(true);
            thisTrans.GetChild(1).gameObject.SetActive(true);
            thisTrans.GetChild(0).GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            thisTrans.GetChild(1).GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            thisTrans.GetChild(0).SetParent(null);
            thisTrans.GetChild(0).SetParent(null);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<MeshRenderer>().enabled = false;
            Die();
        }
    }

    void flapDeath()
    {
        if (!dead)
        {
            dead = true;
            thisTrans.GetChild(2).gameObject.SetActive(true);
            thisTrans.GetChild(2).GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            thisTrans.GetChild(2).SetParent(null);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<MeshRenderer>().enabled = false;
            Invoke("Die", 0.1f);
        }
    }

    void Die()
    {
        currentState = playerStates.dead;
        if (PlayerDead != null) PlayerDead(null, null);
    }
}