using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour {

    public string pName;
    Image im;
    public Sprite spOn, spOff;

	void Start () {
        im = GetComponent<Image>();
        Change();
	}

    void FixedUpdate()
    {
        Change();
    }

    public void Change()
    {
        if (PlayerPrefs.GetInt(pName, 1) == 1)
            im.sprite = spOn;
        else
            im.sprite = spOff;
    }
}
