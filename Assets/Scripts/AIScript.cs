using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

	public GameObject player;

	public bool active;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			GetComponent<NavMeshAgent>().destination = player.transform.position;
		}
	}
}
