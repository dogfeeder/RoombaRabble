using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

    public float moveSpeed = 10;
    public float turnSpeed = 60;
    public float energy = 100;

    public Slider energySlider;

	// Use this for initialization
	void Start () {
        energySlider.value = energy;
    }
	
	// Update is called once per frame
	void Update () {

        //update GUI
        energySlider.value = energy;

        //movement speed if energy available
        if (energy > 0)
        {
            moveSpeed = 10;

        } else
        {
            moveSpeed = 2;
        }

        //movement translations
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, 0);

        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        //lock rotation
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //energy use
        if (Input.GetAxis("Horizontal") != 0 )
        {
            energy -= 0.1f;
        }

        else if (Input.GetAxis("Vertical") != 0)
        {
            energy -= 0.1f;
        }

    }
}
