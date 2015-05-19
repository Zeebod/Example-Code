using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2.5f;
	public float chaseSpeed = 5.0f;
	public float chaseWaitTime = 5.0f;
	public float patrolWaitTime = 1.0f;
	public Transform[] patrolWayPoints;
	
	private EnemySight enemySight;
	private NavMeshAgent nav;
	private Transform player;
	private PlayerHealth playerHealth;
	private LastPlayerSighting lastPlayerSighting;
	private float chaseTimer;
	private float patrolTimer;
	private int wayPointIndex;

	void Awake()
	{
		//Set up our references
		enemySight = GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<DoneLastPlayerSighting>();
		
	}

	void Update()
	{
		//If the player is in sight and is alive...
		if(enemySight.playerInSight && playerHealth.health > 0.0f)
			//we should shoot
			Shooting();
		//If the player has been sighted and isn't dead...
		else if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0.0f)
			//we should chase
			Chasing();
		//Otherwise...
		else
			//we should just patrol
			Patrolling();
	}
	
	void Shootting()
	{
		//Stop the enemy where it is if we are shooting
		nav.Stop();
	}
	
	void Chasing()
	{
		//Create a vector from the enemy to the last sighting of the player
		Vector3 sightDeltaPos = enemySight.personalLastSighting - transform.position;
		
		//If the last personal sighting of the player is not close
		if(sightDeltaPos.sqrMagnitude > 4.0f)
			//set the destination for the NavMeshAgent to the last personal sighting of the player
			nav.destination = enemySight.personalLastSighting;
			
		//set the appropriate speed for the NavMeshAgent
		nav.speed = chaseSpeed;
		
		//If near the last personal sighting
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			//increment the timer
			chaseTimer += Time.deltaTime;
			
			//if the timer exceeds the wait timer
			if(chaseTimer >= chaseWaitTime)
			{
				//reset the last global sighting, the last personal sighting and the timer
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0.0f;
			}
		}
		else
			//if not near the last sighting personal sighting of the player, reset the timer
			chaseTimer = 0.0f;
	}
	
	void Patrolling()
	{
		//set an appropriate speed for the NavMeshAgent
		nav.speed = patrolSpeed;
		
		//If near the next waypoint or there is no destination
		if(nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
		{
			//increment the timer
			patrolTimer += Time.deltaTime;
			
			//if the timer exceeds the wait timer
			if(patrolTimer >= patrolWaitTime)
			{
				//increment the wayPointIndex
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else	
					wayPointIndex++;
				
				//Reset the timer
				patrolTimer = 0;
			}
		}
		else
			//if not near a destination, reset the timer
			patrolTimer = 0;
			
		//set the destination to the patrolWayPoints
		nav.destination = patrolWayPoints[wayPointIndex].position;
	}
}