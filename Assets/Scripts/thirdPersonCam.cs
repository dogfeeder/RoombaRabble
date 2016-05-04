using UnityEngine;
using System.Collections;

public class thirdPersonCam : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        transform.LookAt(player.transform);
    }
}
