using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    AudioSource thisAS;
    public AudioClip[] clip;
    public float volume = 1f;
    bool enab = true;

    void Start () {
        enab = System.Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));
        thisAS = GetComponent<AudioSource>();
        if(clip.Length > 0)
            thisAS.clip = clip[Random.Range(0, clip.Length)];

        CheckMusic();
        thisAS.enabled = true;
    }

    public void ClickMusic()
    {
        enab = !enab;
        PlayerPrefs.SetInt("Sound", System.Convert.ToInt32(enab));
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
