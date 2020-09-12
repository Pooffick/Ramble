using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

    public float speed = 5;
    Vector3 angle;
    public bool rotate = true;
	
    void Start()
    {
        angle = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2));
    }

	void LateUpdate () {
        if (rotate)
            transform.Rotate(angle * speed * Time.deltaTime * 70);
        transform.position += Vector3.down * Time.deltaTime * speed;
    }
}
