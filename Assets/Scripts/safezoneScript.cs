using UnityEngine;
using System.Collections;

public class safezoneScript : MonoBehaviour {
	private GameObject AI;
	// Use this for initialization
	void Start () {
		AI = GameObject.FindGameObjectWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			AI.GetComponent<EnemyAI> ().attack = false;
			Debug.Log ("AI chase off");
		}
	}
}
