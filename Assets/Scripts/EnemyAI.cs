using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private Transform myTransform;

	private NavMeshAgent agent;

	//Patrol points
	public GameObject[] patrolPoints;
	public int currentPatrolPoint = 0;
	private float patrolPointDistance = 1.0f;

	// Use this for initialization
	void Start () {

		myTransform = this.transform;

		agent = GetComponentInChildren<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

		CyclePatrolling();
	}

	//In-order patrol technique: will patrol points in sequence, looping back to the first at the end
	void CyclePatrolling(){

		//Move towards current patrol point
		agent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);

		//Close to/arrived at patrol point. Switch to next/first patrol point
		if (Vector3.Distance (myTransform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolPointDistance) {

			if(currentPatrolPoint == patrolPoints.Length - 1)
				currentPatrolPoint = 0;
			else
				currentPatrolPoint++;
		}
	}
}
