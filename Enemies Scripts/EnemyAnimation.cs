using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (AudioSource))]
public class EnemyAnimation : MonoBehaviour {

	public AudioClip attack;

	Animator anim;
	NavMeshAgent agent;
	AudioSource playerAudio;
	bool playerInRange;
	bool isStopped = false;
	float iSpeed;

	void Start () {
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		playerAudio = GetComponent<AudioSource> ();
		agent.updatePosition = false;
		iSpeed = agent.speed;
	}

	public void SetPlayerInRange(bool playerR) {
		playerInRange = playerR;
		if (playerR) {
			StartCoroutine(PlayAudio(attack));
		}
	}

	public void SetStopped(bool isStop) {
		isStopped = isStop;
	}

	void Update () {
		anim.SetBool ("Stop", isStopped);
		if (!isStopped) {
			anim.SetBool ("Attack", playerInRange);
			anim.SetFloat ("MoveSpeed", agent.velocity.magnitude);
		} else {
			anim.SetBool ("Attack", false);
			anim.SetFloat ("MoveSpeed", 0);
		}
	}

	void OnAnimatorMove () {
		if (!isStopped) {
			transform.position = agent.nextPosition;
		}
	}

	IEnumerator PlayAudio (AudioClip audioClip) {
		playerAudio.clip = audioClip;
		playerAudio.Play();
		yield return new WaitForSeconds(audioClip.length);
	}
}