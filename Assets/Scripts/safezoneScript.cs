using UnityEngine;
using System.Collections;

public class safezoneScript : MonoBehaviour {
	public GameObject AI;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			AI.GetComponent<AIScript> ().active = false;
			Debug.Log ("AI chase off");
		}
	}
}
