using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public Slider Volume;
    public AudioSource mainMenuMusic;
	
	// Update is called once per frame
	void Update () {
        mainMenuMusic.volume = Volume.value;
	}
}
