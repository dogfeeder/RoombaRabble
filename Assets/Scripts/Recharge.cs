using UnityEngine;
using System.Collections;

public class Recharge : MonoBehaviour {

	public GameObject AI;

	public bool charging;
	private GameObject collision;
    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (charging) {
            if (player.GetComponent<player>().energy < 100.0f)
            {
                player.GetComponent<player>().energy += 0.1f;
            }
		}
	}

	private void OnTriggerEnter(Collider other) {
		charging = true;
		AI.GetComponent<AIScript> ().active = true;
	}

	private void OnTriggerExit(Collider other) {
		charging = false;
	}
}
