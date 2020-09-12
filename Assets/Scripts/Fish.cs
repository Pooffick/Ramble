using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

    public float speed = 5;
    bool right;
	
    void Start()
    {
        if (transform.position.x < 0)
            right = true;
        else
        {
            right = false;
            transform.Rotate(Vector3.up * 180);
        }

        speed += Random.Range(-0.9f, 1f);
    }
    
	void Update () {
        if(right)
            transform.position += Vector3.right * Time.deltaTime * speed;
        else
            transform.position += Vector3.left * Time.deltaTime * speed;
    }
}
