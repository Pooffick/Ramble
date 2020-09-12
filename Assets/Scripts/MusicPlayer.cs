using UnityEngine;
using System.Collections;


[RequireComponent (typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    AudioSource thisAS;
    public AudioClip[] clip;
    public float volume = 0.7f;
    bool enab = true;

	void Start () {
        enab = System.Convert.ToBoolean(PlayerPrefs.GetInt("Music", 1));
        thisAS = GetComponent<AudioSource>();
        thisAS.clip = clip[Random.Range(0, clip.Length)];

        CheckMusic();
        thisAS.enabled = true;
    }

    public void ClickMusic()
    {
        enab = !enab;
        PlayerPrefs.SetInt("Music", System.Convert.ToInt32(enab));
        CheckMusic();
    }

    void CheckMusic()
    {
        if (enab)
            thisAS.volume = volume;
        else
            thisAS.volume = 0;
    }
}
