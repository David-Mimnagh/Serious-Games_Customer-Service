using UnityEngine;
using System.Collections;

public class MusicManagerScipt : MonoBehaviour {

    public AudioClip[] songs;
    int currentSong = 0;
    AudioSource audio;
	// Use this for initialization
	void Start () {
       audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (audio.isPlaying == false)
        {
            currentSong++;
            if (currentSong >= songs.Length)
                currentSong = 0;

            audio.clip = songs[currentSong];
            audio.Play();
        }
	}
}
