using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed = 5;
    public Vector3 angle;

    void LateUpdate()
    {
        transform.Rotate(angle * speed * Time.deltaTime * 70);
    }
}
