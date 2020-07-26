using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class FollowPlayer : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;
	float updateDelay = .3f;

	void Awake () {
		//link with player tagged
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if(!player){
			Debug.Log("Is your player tagged?");
		}
		agent = GetComponent<NavMeshAgent> ();
	}

	void Start () {
		InvokeRepeating ("FollowTarget", 0f, updateDelay);
	}

	void FollowTarget () {
		// Put destiny enemy
		agent.SetDestination(player.position);
	}
}
