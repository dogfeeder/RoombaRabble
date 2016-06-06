using UnityEngine;
using System.Collections;

public class TileCode : MonoBehaviour {
	
	private Renderer rend;

	private float timer = 0.2f;
	private float currentTime;

	private bool playerTouching;
    private bool enemyTouching;
	public bool clean;

    public bool triggered;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
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

	void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            playerTouching = true;
        }
        if (other.tag == "CatHit")
        {
            if (!triggered)
            {
                int random = Random.Range(0, 10000);
                if (random < 50)
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
