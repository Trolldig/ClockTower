using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBehavior : MonoBehaviour {

	public GameObject spikes;
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider col){
		Debug.Log("oof");
		spikes.SetActive(true);
	}
}
