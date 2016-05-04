using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] tiles;
	public int tileCount = 0;
	public Text tileCountText;

	// Use this for initialization
	void Start () {
		tiles = GameObject.FindGameObjectsWithTag ("tile");
		tileCount = tiles.Length;
		tileCountText.text = tileCount.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        tiles = GameObject.FindGameObjectsWithTag("tile");
        tileCount = tiles.Length;
		//Debug.Log (tileCount);
		tileCountText.text = tileCount.ToString ();
	}
		
}
