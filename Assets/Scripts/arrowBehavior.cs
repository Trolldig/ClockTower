using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowBehavior : MonoBehaviour {

	public GameObject arrow;

	void OnTriggerEnter(){
		arrow.SetActive(true);
	}
}
