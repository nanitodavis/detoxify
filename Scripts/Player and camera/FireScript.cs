using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
	public Rigidbody m_Shell;                   // Prefab of the shell.
	public Rigidbody m_Shell2;
	public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
	public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
	public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
	public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
	public float vel = 20f;        // The force given to the shell if the fire button is not held.
	public int demage = 5;





	private void Start ()
	{

		m_ShootingAudio = GetComponent<AudioSource> ();
		// The fire axis is based on the player number.
		//m_FireButton = "Fire" ;

		// The rate that the launch force charges up is the range of possible forces by the max charge time.
		//m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
	}


	private void Update ()
	{
		if (Input.GetMouseButtonDown (0))
			Fire ();
		if (Input.GetMouseButtonDown (1))
			Fire2 ();

	}


	private void Fire ()
	{
		// Set the fired flag so only Fire is only called once.
		// m_Fired = true;

		// Create an instance of the shell and store a reference to it's rigidbody.
		Rigidbody shellInstance =
			Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		m_ShootingAudio.volume=0.3f;
		m_ShootingAudio.clip = m_ChargingClip;
		m_ShootingAudio.Play ();
		shellInstance.velocity = vel * m_FireTransform.forward; 

	}

	private void Fire2 ()
	{
		// Set the fired flag so only Fire is only called once.
		// m_Fired = true;

		// Create an instance of the shell and store a reference to it's rigidbody.
		Rigidbody shellInstance =
			Instantiate (m_Shell2, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
			shellInstance.velocity = vel * m_FireTransform.forward; 

			m_ShootingAudio.volume=0.3f;
			m_ShootingAudio.clip = m_FireClip;
			m_ShootingAudio.Play ();

	}
}