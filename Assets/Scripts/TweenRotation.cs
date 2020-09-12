using UnityEngine;
using System.Collections;

public class TweenRotation : MonoBehaviour {

    public Vector3 to;
    float timer = 0;
    public float speed = 3f;
    int goBack = 1;
    Transform thisTrans;
    
    void Start () {
        thisTrans = transform;
	}
	
	void Update () {
        if (timer <= 0 && goBack == -1)
            goBack = 1;

        if (timer >= 1 && goBack == 1)
            goBack = -1;

        timer += Time.deltaTime * goBack * speed;

        thisTrans.localEulerAngles = Vector3.Lerp(-to, to, timer);
    }
}
