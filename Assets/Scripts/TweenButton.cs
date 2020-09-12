using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TweenButton : MonoBehaviour {

    RectTransform rTform;
    public Vector3 scaleTo;
    int goBack = 1;
    float timer = 0;
    public float speed = 10;

    void Start () {
        rTform = GetComponent<RectTransform>();
	}
	
    void Update()
    {
        if (timer <= 0 && goBack == -1)
            goBack = 1;

        if (timer >= 1 && goBack == 1)
            goBack = -1;

        timer += Time.deltaTime * goBack * speed;

        rTform.localScale = Vector3.Lerp(Vector3.one, scaleTo, timer);
    }
}
