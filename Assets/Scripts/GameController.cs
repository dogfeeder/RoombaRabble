using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] tiles;
	public static int tileCount = 0;
	public static int cleanTiles = 0;
	public Text tileCountText;	

	// Use this for initialization
	void Start () {
		cleanTiles = 0;
		tiles = GameObject.FindGameObjectsWithTag ("tile");
		tileCount = tiles.Length;
		tileCountText.text = tileCount.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        tiles = GameObject.FindGameObjectsWithTag("tile");
		tileCount = tiles.Length - cleanTiles;
		tileCountText.text = tileCount.ToString ();

		if (cleanTiles == tileCount) {
			RestartGame ();
		}

	}
		
	public static void RestartGame() {
		//Scene scene = SceneManager.GetActiveScene();
		//SceneManager.LoadScene(scene.name);
	}
		
}
