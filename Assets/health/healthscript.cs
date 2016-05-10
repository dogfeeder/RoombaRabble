using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthscript : MonoBehaviour {

	public Sprite[] heartSprites;
	public Image HeartUI;
	public int startingHealth = 3;
	public int currentHealth;
	GameObject enemy;
	public float timeBetweenAttacks = 1.0f;
	public float currentTime = 0;
	public bool triggered;



	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		HeartUI.sprite = heartSprites[currentHealth];	

		if (triggered) {
			currentTime += Time.deltaTime;
			Debug.Log (currentTime);
			if (currentTime > timeBetweenAttacks) {
				triggered = false;
				currentTime = 0;
			}
		}
	}

	public void TakeDamage (int amount){
		if (currentHealth >0) {
			currentHealth -= amount;
		}
		if (currentHealth <= 0) {
			GameController.RestartGame ();

		}
	}

	void OnTriggerEnter (Collider other){
		if (!triggered) {
			if (other.gameObject == enemy) {
				triggered = true;
				TakeDamage(1);
			}
		}

	}
		

}

