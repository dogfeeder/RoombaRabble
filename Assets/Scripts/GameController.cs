using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static int difficulty;

	public GameObject[] tiles;
	public static int tileCount = 0;
	public static int cleanTiles = 0;
	public Text tileCountText;

    public GameObject winGUI;
    public GameObject menuGUI;
    public Text timerGUI;
    public Text timeTakenGUI;
    public float timer = 0;
    public bool gameOver;

    public AudioClip win;
    private bool played = false;
    private bool showingMainMenu;
    private float winPercentage;
    private float percentageCalc;

    private int totalTiles;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        cleanTiles = 0;
		tiles = GameObject.FindGameObjectsWithTag ("tile");
        totalTiles = tiles.Length;
        tileCount = tiles.Length;
		tileCountText.text = tileCount.ToString();

        switch (difficulty)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {

        //MainMenu showing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (showingMainMenu)
            {
                hideMainMenu();
            }
            else
            {
                showMainMenu();
            }
        }

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
		//tileCountText.text = "Tiles Left: " + tileCount.ToString() + "/" + totalTiles.ToString ();
		percentageCalc = 100 - (float)tileCount / (float)totalTiles * 100;
		percentageCalc = Mathf.Floor (percentageCalc);
		tileCountText.text = percentageCalc.ToString() + "% Clean";

		if (cleanTiles == totalTiles) {
            winGUI.SetActive(true);
            if (!played)
            {
                GetComponent<AudioSource>().PlayOneShot(win, 1.0f);
                GameObject.Find("Player").GetComponent<AudioSource>().Stop();
                played = true;
            }
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

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void showMainMenu()
    {
        menuGUI.SetActive(true);
        showingMainMenu = true;
        Time.timeScale = 0;
    }

    public void hideMainMenu()
    {
        menuGUI.SetActive(false);
        showingMainMenu = false;
        Time.timeScale = 1;
    }

}
