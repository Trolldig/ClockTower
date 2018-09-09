using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAudioManager : MonoBehaviour {
    public Slider Volume;
    public AudioSource LevelMusic;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LevelMusic.volume = Volume.value;
    }
}
