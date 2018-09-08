using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowFly : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		gameObject.transform.position = gameObject.transform.position + new Vector3(-0.5f,0,0);
	}
	void OnTriggerEnter(Collider col){
		if(col.tag == "Player" || col.gameObject.layer == 9){
			gameObject.SetActive(false);
		}
	}
}
