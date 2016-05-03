using UnityEngine;
using System.Collections;

public class TileCode : MonoBehaviour {
	
	private Renderer rend;

	private float timer = 1;
	private float currentTime;

	private bool touching;
	private bool clean;
	private bool removed;

	public GameObject gc;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.grey;
	}

	// Update is called once per frame
	void Update () {
		if (touching) {
			currentTime += Time.deltaTime;
			if (currentTime > timer) {
				rend.material.color = Color.white;
				clean = true;
			}
		}

		if (clean && !removed) {
			gc.GetComponent<GameController>().tileCount -= 1;
			removed = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		touching = true;
	}

	void OnTriggerExit(Collider other) {
		touching = false;
		currentTime = 0;
	}
}
