using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

    public float moveSpeed = 6;
    public float turnSpeed = 100;
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
        if (energy > 10)
        {
            moveSpeed = 10;

        } else
        {
            moveSpeed = 2;
        }

        //movement translations
        float horizontal = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        //lock rotation
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        //energy use
        if (Input.GetAxis("Horizontal") != 0 )
        {
            if (energy > 0)
            {
                //energy -= 0.1f;
            }
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            if (energy > 0)
            {
                energy -= 0.05f;
            }
        }

    }
}
