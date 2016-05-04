using UnityEngine;
using System.Collections;

public class TileCode : MonoBehaviour {
	
	private Renderer rend;

	private float timer = 0.2f;
	private float currentTime;

	private bool playerTouching;
    private bool enemyTouching;
	private bool clean;

	public GameObject gc;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.grey;
	}

	// Update is called once per frame
	void Update () {
		if (playerTouching) {
			currentTime += Time.deltaTime;
            if (currentTime > timer)
            {
                rend.material.color = Color.white;
                clean = true;
            }
		}
    }

	void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            playerTouching = true;
            Debug.Log("PLAY Touched");
        }
        if (other.tag == "Enemy")
        {
            enemyTouching = true;
            rend.material.color = Color.grey;
            Debug.Log("CAT Touched");
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
