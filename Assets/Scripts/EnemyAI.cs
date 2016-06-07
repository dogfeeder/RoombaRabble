using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

	private NavMeshAgent agent;

    //Patrol points
    public GameObject[] patrolPoints;
	public int currentPatrolPoint = 0;
	private float patrolPointDistance = 1.0f;

    AudioSource audioSource;
    public AudioClip meow;
    private bool played;

    // Use this for initialization
    public bool playerInRange; // is the player within the enemy's sight range collider (this only checks if the enemy can theoretically see the player if nothing is in the way)

    [SerializeField]
    Transform lineOfSightEnd;
    public GameObject player;
    Transform playerTransform; // a reference to the player for raycasting
    public bool canSee;
    public bool attack;

    // Use this for initialization
    void Start () {

		agent = GetComponentInChildren<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        playerInRange = false;
        playerTransform = player.transform;
    }

    void FixedUpdate()
    {
        if (CanPlayerBeSeen())
        {
            Debug.Log("I can see!!!!");
            canSee = true;
}

        else
        {
            canSee = false;
        }

    }

    // Update is called once per frame
    void Update () {

        if (attack || canSee)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
            if (!played) {
                if (attack)
                {
                    audioSource.PlayOneShot(meow, 1.0f);
                    played = true;
                }
            }
        }
        else
        {

            CyclePatrolling();
            played = false;
        }
	}

	//In-order patrol technique: will patrol points in sequence, looping back to the first at the end
	void CyclePatrolling(){

		//Move towards current patrol point
		agent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);

		//Close to/arrived at patrol point. Switch to next/first patrol point
		if (Vector3.Distance (transform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolPointDistance) {

			if(currentPatrolPoint == patrolPoints.Length - 1)
				currentPatrolPoint = 0;
			else
				currentPatrolPoint++;
		}
	}

    bool CanPlayerBeSeen()
    {
        // we only need to check visibility if the player is within the enemy's visual range
        if (playerInRange)
        {
            if (PlayerInFieldOfView())
            {
                Debug.Log("In Field of View");
                return (!PlayerHiddenByObstacles());
            }
            else
            {
                return false;
            }

        }
        else
        {
            // always false if the player is not within the enemy's range
            return false;
        }

        //return playerInRange;

    }
    void OnTriggerStay(Collider other)
    {
        // if 'other' is player, the player is seen 
        // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
        if (other.transform.tag == "Player")
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        // if 'other' is player, the player is seen
        // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
        if (other.transform.tag == "Player")
            playerInRange = false;
    }

    bool PlayerInFieldOfView()
    {
        // check if the player is within the enemy's field of view
        // this is only checked if the player is within the enemy's sight range

        // find the angle between the enemy's 'forward' direction and the player's location and return true if it's within 65 degrees (for 130 degree field of view)

        Vector3 directionToPlayer = playerTransform.position - transform.position; // represents the direction from the enemy to the player    
        Debug.DrawLine(transform.position, playerTransform.position, Color.magenta); // a line drawn in the Scene window equivalent to directionToPlayer

        Vector3 lineOfSight = lineOfSightEnd.position - transform.position; // the centre of the enemy's field of view, the direction of looking directly ahead
        Debug.DrawLine(transform.position, lineOfSightEnd.position, Color.red); // a line drawn in the Scene window equivalent to the enemy's field of view centre

        // calculate the angle formed between the player's position and the centre of the enemy's line of sight
        float angle = Vector3.Angle(directionToPlayer, lineOfSight);

        // if the player is within 65 degrees (either direction) of the enemy's centre of vision (i.e. within a 130 degree cone whose centre is directly ahead of the enemy) return true
        if (angle < 75)
            return true;
        else
            return false;
    }

    bool PlayerHiddenByObstacles()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, playerTransform.position - transform.position, distanceToPlayer);
        Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking
        List<float> distances = new List<float>();

        foreach (RaycastHit hit in hits)
        {
            // ignore the enemy's own colliders (and other enemies)
            if (hit.transform.tag == "Enemy")
                continue;

            // if anything other than the player is hit then it must be between the player and the enemy's eyes (since the player can only see as far as the player)
            if (hit.transform.tag != "Player")
            {
                return true;
            }
        }

        // if no objects were closer to the enemy than the player return false (player is not hidden by an object)
        return false;

    }
}
