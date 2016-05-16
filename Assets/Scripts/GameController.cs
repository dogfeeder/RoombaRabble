using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] tiles;
	public static int tileCount = 0;
	public static int cleanTiles = 0;
	public Text tileCountText;

    public GameObject winGUI;
    public Text timerGUI;
    public Text timeTakenGUI;
    public float timer = 0;
    public bool gameOver;

    private int totalTiles;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        cleanTiles = 0;
		tiles = GameObject.FindGameObjectsWithTag ("tile");
        totalTiles = tiles.Length;
        tileCount = tiles.Length;
		tileCountText.text = tileCount.ToString();
	}
	
	// Update is called once per frame
	void Update () {

        //Timer Stuff
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");

        if (!gameOver)
        {
            timer += Time.deltaTime;
            timerGUI.text = minutes + ":" + seconds;
        } else
        {
            timeTakenGUI.text = "Time Taken: " + minutes + ":" + seconds;
        }

        //Finding Tiles
        tiles = GameObject.FindGameObjectsWithTag("tile");
		tileCount = tiles.Length - cleanTiles;
		tileCountText.text = "Tiles Left: " + tileCount.ToString() + "/" + totalTiles.ToString ();

		if (cleanTiles == totalTiles) {
            winGUI.SetActive(true);
            gameOver = true;
            Time.timeScale = 0;
		}

	}
	//Restart Game
	public void RestartGame() {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
    }
		
}
