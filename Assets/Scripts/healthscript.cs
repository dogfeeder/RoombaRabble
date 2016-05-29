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

    public AudioClip meow;
    public AudioClip dead;

    public GameObject loseGUI;

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
            loseGUI.SetActive(true);
            enemy.GetComponent<AudioSource>().PlayOneShot(dead, 1.0f);
            Time.timeScale = 0;

        }
	}

	void OnTriggerEnter (Collider other){
		if (!triggered) {
			if (other.gameObject == enemy) {
				triggered = true;
                enemy.GetComponent<AudioSource>().PlayOneShot(meow,1.0f);
				TakeDamage(1);
			}
		}

	}
		

}

