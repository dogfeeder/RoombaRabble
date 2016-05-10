using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthscript : MonoBehaviour {

	public Sprite[] heartSprites;
	public Image HeartUI;
	public int startingHealth = 3;
	public int currentHealth;
	GameObject enemy;
	public float timeBetweenAttacks = 3.0f;
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
				Debug.Log ("Done");
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
			Debug.Log ("player is dead");
		}
	}

	void OnTriggerEnter (Collider other){
		if (!triggered) {
			Debug.Log (triggered);
			if (other.gameObject == enemy) {
				triggered = true;
				TakeDamage(1);
				//StartCoroutine (attackTimer (3.0F));
			}
		}

	}

	IEnumerator attackTimer(float waittime){
		TakeDamage (1);
		Debug.Log ("1hit");
		yield return new WaitForSeconds(waittime);

	}
		

}

