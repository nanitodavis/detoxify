using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyFollowPlayer : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;
	float updateDelay = .3f;
	bool isStopped;

	void Awake () {
		//link with player tagged
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if(!player){
			Debug.Log("Is your player tagged?");
		}
		agent = GetComponent<NavMeshAgent> ();
	}

	public void SetStopped(bool isStop) {
		isStopped = isStop;
	}

	void Start () {
		InvokeRepeating ("FollowTarget", 0f, updateDelay);
	}

	void FollowTarget () {
		if (!isStopped) {
			// Put destiny enemy
			agent.SetDestination (player.position);
		} else {
			agent.SetDestination (this.transform.position);
		}
	}
}
