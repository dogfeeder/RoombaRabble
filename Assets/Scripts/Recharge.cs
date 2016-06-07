using UnityEngine;
using System.Collections;

public class Recharge : MonoBehaviour {

	public GameObject AI;

	public bool charging;
    public GameObject player;

    private AudioSource audioSource;
    public AudioClip beep;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (charging) {
            if (player.GetComponent<CharacterControllercs>().energy < 99.9f)
            {
                player.GetComponent<CharacterControllercs>().energy += 0.2f;
            }
		}
	}

	private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            audioSource.PlayOneShot(beep, 1.0f);
            charging = true;
            AI.GetComponent<EnemyAI>().attack = true;
        }
	}

	private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            charging = false;
        }
	}
}
