using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraScript : MonoBehaviour {

	public Transform target; //target that the camera will follow
	private Vector3 offset;
	private Vector3 targetPos;
	private float horizontal;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		offset = transform.position - target.position;//offset between camera and target
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.transform.position+(offset);
		transform.LookAt (target.transform);
	}
}
