using UnityEngine;
using System.Collections;

public class RugCode : MonoBehaviour {

	private Renderer rend;

	private float timer = 0.2f;
	private float currentTime;

	private bool playerTouching;
	private bool enemyTouching;
	public bool clean;


	private float rugTimer = 0;
	public float rugCleanTime;
	public Color colorStart = Color.clear;
	public Color colorEnd = Color.white;
	public float fadeOutDuration = 3;

	// Use this for initialization
	void Start () {
		rend = this.GetComponent<Renderer>();
		rend.material.color = Color.grey;
	}

	// Update is called once per frame
	void Update () {
		if (playerTouching && !clean) {
			currentTime += Time.deltaTime;
			if (currentTime > timer)
			{
				currentTime = 0;
				rend.material.color = Color.white;
				clean = true;
				GameController.cleanTiles++;
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player")
		{
			if(!clean)
			{
				float lerp = Mathf.PingPong (Time.time, fadeOutDuration) / fadeOutDuration;
				this.rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
			}

			rugTimer += Time.deltaTime;
			if (rugTimer > rugCleanTime) {
				playerTouching = true;
			}
		}
		if (other.tag == "Enemy")
		{
			if (clean)
			{
				GameController.cleanTiles--;
			}
			enemyTouching = true;
			clean = false;
			rend.material.color = Color.grey;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player")
		{
			playerTouching = false;
			currentTime = 0;
		}
		else
		{
			enemyTouching = false;
		}
	}


}