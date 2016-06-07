/// <summary>
/// Charactercontrollercs.cs
/// Character Controller in CSharp
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterControllercs : MonoBehaviour {
			
	//Var definition
	public string moveStatus = "idle";				//movestatus for animations
	
	//Movement speeds
	private float gravity = 20.0f;					//Gravity for jump
	private float runSpeed = 6.0f;					//Speed when the Character is running
	private float walkSpeed = 4.0f;					//Speed when the Character is walking (normal movement)
	private float rotateSpeed = 120.0f;				//Rotationspeed of the Character
	private float walkBackMod = 0.75f;				//Speed in Percent for walk backwards and sidewalk
	
	//Internal vars to work with
	private float speedMod = 0.0f;					//temp Var for Speedcalculation
	private bool grounded = false;					//temp var if the character is grounded
	private Vector3 moveDirection = Vector3.zero;	//move direction of the Character
	private bool isWalking = false;					//toggle var between move and run
	private CharacterController controller;			//CharacterController for movement

    public float energy = 100;
    public Slider energySlider;
    public Text energyText;
    public RectTransform sliderFill;

	public GameObject loseGUI;

	private CharacterController c;

    private AudioSource audioSource;
    public AudioClip buzz;
    private bool isPlaying;

    void Start()
    {
        energySlider.value = energy;
        energyText.text = "Energy: " + energy + "%";
		c = GetComponent<CharacterController>();
		c.detectCollisions = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = buzz;

    }
	
	//Every Frame
    void Update()
    {
        //update GUI
        energySlider.value = energy;
        energyText.text = "Energy: " + Math.Round(energy,1) + "%";

        //movement speed if energy available
        if (energy > 50) {
            walkSpeed = 4.0f;
            runSpeed = 6.0f;
            sliderFill.GetComponent<Image>().color = new Color(0.000f, 0.926f, 0.160f, 1.000f);
        } else if (energy > 10) {
            walkSpeed = 4.0f;
            runSpeed = 6.0f;
            sliderFill.GetComponent<Image>().color = new Color(0.926f, 0.709f, 0.000f, 1.000f);
        } else if (energy > 0) {
			walkSpeed = 2.0f;
			runSpeed = 3.0f;
			sliderFill.GetComponent<Image> ().color = Color.red;
		} else {
			loseGUI.SetActive(true);
			Time.timeScale = 0;
		}

        //Set idel animation
        moveStatus = "idle";
        isWalking = true;

        // Hold "Run" to run
        if (Input.GetAxis("Run") != 0)
            isWalking = false;

        // Only allow movement and jumps while grounded
        if (grounded)
        {

            //movedirection
            moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));

            //L+R MouseButton Movement
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1) && (Input.GetAxis("Vertical") == 0))
                moveDirection.z += 1;

            if (moveDirection.z > 1)
                moveDirection.z = 1;

            // if moving forward and to the side at the same time, compensate for distance
            if (Input.GetMouseButton(1) && (Input.GetAxis("Horizontal") != 0) && (Input.GetAxis("Vertical") != 0))
            {
                moveDirection *= 0.7f;
            }

            //Speedmodification / is moving forward or side/backward
            speedMod = ((Input.GetAxis("Vertical") < 0) || (Input.GetMouseButton(1) && (Input.GetAxis("Horizontal")) != 0)) ? walkBackMod : 1.0f;

            //Use run or walkspeed
            moveDirection *= isWalking ? walkSpeed * speedMod : runSpeed * speedMod;


            //movestatus normal movement (for animations)      		
            if ((moveDirection.x == 0) && (moveDirection.z == 0))
                moveStatus = "idle";
            if (moveDirection.z > 0)
                moveStatus = isWalking ? "walking" : "running";
            if (moveDirection.z < 0)
                moveStatus = isWalking ? "backwalking" : "backrunning";
            if (moveDirection.x > 0)
                moveStatus = isWalking ? "sidewalking_r" : "siderunning_r";
            if (moveDirection.x < 0)
                moveStatus = isWalking ? "sidewalking_l" : "siderunning_l";

            if (moveStatus != "idle" && energy > 0)
            {
                if (!isPlaying)
                {
                    audioSource.Play();
                    isPlaying = true;
                }
                if (moveStatus == "running")
                {
                    energy -= 0.08f;
                } else if (moveStatus == "idle") {

                } else
                {
                    energy -= 0.04f;
                }
            } else
            {
                audioSource.Stop();
                isPlaying = false;
            }


            //transform direction
            moveDirection = transform.TransformDirection(moveDirection);

        }
        // Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.
        if (Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
        }

        //Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //Get CharacterController
        controller = GetComponent<CharacterController>();
        //Move Charactercontroller and check if grounded
        grounded = ((controller.Move(moveDirection * Time.deltaTime)) & CollisionFlags.Below) != 0;
    }
}
