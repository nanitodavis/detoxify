using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour {

	public Transform myTransform;
	public float velocity = 0.5f;
	public Animator anim;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)){
			myTransform.position += transform.forward * velocity;
			anim.SetBool ("walk", true);
		}
		else if(Input.GetKey(KeyCode.S)){
			myTransform.position -= transform.forward * velocity;
			anim.SetBool ("walk", true);
		}

		if(Input.GetKey(KeyCode.A)){
			myTransform.position -= transform.right * velocity;
			anim.SetBool ("walk", true);
		}
		else if(Input.GetKey(KeyCode.D)){
			myTransform.position += transform.right * velocity;
			anim.SetBool ("walk", true);
		}

	}
}
