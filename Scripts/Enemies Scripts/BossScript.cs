using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

	//Boss Properties, such as health and attack damage
	public float aux = 0; //auxiliar int for calculating attacks
	public int health = 200; //boss health
	private int attackPower = 10; //boss attack Power or damage inflicted to target
	private int defense = 5; // Boss resistance against attacks
	private float damageToRecoil = 20f;// when the numbered units of damage is taken, boss will recoil backwards
	private Vector3 velocity = Vector3.zero; // Boss movement reference
	private float speed = 1.5f;
	private bool transformationOn = false; //to know if the boss already transform
	private bool isDead = false;
	private float followPlayerRange = 50f; 
	private float attackPlayerRange = 7f;
	private float attackDelay = 1f; //delay between attacks
	public float attackDelayCount = 0f;
	private bool inFollowRange = false; //to know if the target is in the boss range to follow
	private bool inAttackRange = true; //to know if the target is in the boss range to attack
	public bool isHabilityActive = false; //used to use habilities
	public bool habilityOneOn=false; //used to use hability 1
	public bool habilityTwoOn=false; //used to use hability 2
	private Vector3 targetedPosition = Vector3.zero; //targeted position for hability two
	private bool noticePlayer = false;
	private bool targetDestination = false;
	private float randomHability=0;
	private bool paralized = false;
	private int paralisysdHealth = 20;

	//GameObject components
	//public Rigidbody rb; //cache for rigidbody
	public Transform myTransform; //cache for transform
	public Transform target; // target actual transform
	public Animation anim;
	public AudioSource aSource;

	//AudioClips
	public AudioClip idleSound;
	public AudioClip attackOne;
	public AudioClip attackTwo;
	public AudioClip damageReceived;
	public AudioClip transformationSound;
	public AudioClip death;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animation> ();
		//rb = GetComponent<Rigidbody> ();
		myTransform = GetComponent<Transform> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		aSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		aux += Time.deltaTime;
		if (paralized) {
			StartCoroutine ("TemporalParalized");
		}
			
		else if (Vector3.Distance (target.position, myTransform.position) <= 100f) {
			if (!isDead && !isHabilityActive) {
				if (Random.Range (0, 100) <= 0.1f && !isHabilityActive&&noticePlayer)
					isHabilityActive = true;
				if (health <= 0) {
					StartCoroutine ("Die");
				}

			//if the health drop bellow 40 execute the transformation methond and set transformationOn to true
			else if (health <= 40 && (!transformationOn)) {
					StartCoroutine ("Transformation");
				}

			//conditional used to detect player in range to attack him
			else if (Vector3.Distance (target.position, myTransform.position) <= attackPlayerRange) {

					//Start the Coroutine to attack the target
					if (aux >= attackDelayCount) {
						Attack ();
						attackDelayCount = aux + attackDelay;
					}
				}

			//conditional used to detect player in range to follow him
			else if (Vector3.Distance (target.position, myTransform.position) <= followPlayerRange) {

					//Start the Follow method
					FollowPlayer ();
					noticePlayer = true;
					if (!aSource.isPlaying) {
						SetIdleSound ();
					}
				} else {

					//if no detection occurs, just play idle animation
					anim.Play ("idle");
					SetIdleSound ();

				}
			} else {
				if (!habilityOneOn && !habilityTwoOn) {
					randomHability = Random.Range (0, 10);
				} 
				if (isHabilityActive) {
					if (randomHability <= 3) {
						StartCoroutine ("HabilityOne");
					} else {
						StartCoroutine ("HabilityTwo");
					}
				}
			}
		} else {
			anim.Play ("idle");
			SetIdleSound ();
		}
	}

	//method for moving the boss
	void FollowPlayer(){
		myTransform.LookAt (target.position);//look towards the target

		myTransform.position = Vector3.SmoothDamp (myTransform.position, target.position, ref velocity, speed);

		//play the walk anim once
		anim ["walk"].wrapMode = WrapMode.Once;
		anim.Play ("walk");
	}

	//method used to attack the target
	void Attack(){

		//first section is to randomly use one of the 2 attack animation
		string attackAnim = "hit";
		if (Random.Range (0f, 2f) <= 1) {
			attackAnim = "hit";
			aSource.clip = attackOne;
		} else {
			attackAnim = "hit2";
			aSource.clip = attackTwo;
		}
		aSource.loop = false;
		//play the animation if no animation is playing
		anim [attackAnim].wrapMode = WrapMode.Once;
		anim.Play (attackAnim);
		aSource.Play ();

	}

	IEnumerator TemporalParalized(){
		anim.Play ("idle");
		SetIdleSound ();
		yield return new WaitForSeconds (1f);
		paralized = false;
		paralisysdHealth = 20 + defense;
	}

	//mehtod used for transform when the health is below 40%
	IEnumerator Transformation(){
		anim ["rage"].wrapMode = WrapMode.Once;
		anim.Play ("rage");
		defense = 10;
		attackPower = 15;
		speed = 0.8f;
		attackDelay = 0.8f;
		if (!aSource.isPlaying) {
			aSource.clip = transformationSound;
			aSource.pitch = 0.6f;
			aSource.loop = false;
			aSource.Play ();
		}
		yield return new WaitForSeconds (2.2f);
		health = 120;
		transformationOn = true;
		SetIdleSound ();
		anim.PlayQueued ("idle");
	}

	IEnumerator Die(){
		if (!isDead) {
			isDead = true;
			aSource.clip = death;
			aSource.pitch = 0.6f;
			aSource.loop = false;
			aSource.Play ();
			anim ["die"].wrapMode = WrapMode.Once;
			anim.Play ("die");
			yield return new WaitForSeconds (3f);
			aSource.mute = true;
			Destroy (this.gameObject);
		}
	}


	//public method for taking damage from player
	public void TakeDamage(int damage){
		health -= damage;
	}

	public void TakeParalisysDamage(int damage){
		paralisysdHealth -= damage;
	}

	//IENumerator for casting Hability One
	IEnumerator HabilityOne(){
		if (!habilityOneOn) {
			myTransform.position = target.position;
			attackDelay = 0.7f;
			followPlayerRange = -1f;
			habilityOneOn = true;
		}
		if (aux >= attackDelayCount) {
			Attack ();
			attackDelayCount = aux + attackDelay;
		}
		yield return new WaitForSeconds (2f);
		if (transformationOn) {
			attackDelay = 0.8f;
		} else {
			attackDelay=1f;
		}
		followPlayerRange = 50f;
		isHabilityActive = false;
		habilityOneOn = false;
	}

	//IEnumerator for casting hability two
	IEnumerator HabilityTwo(){
		if (!habilityTwoOn) {
			attackPower = 25;
			speed = 1f;
			habilityTwoOn = true;
			myTransform.LookAt (target);
			targetedPosition = target.position-Vector3.one;
		}
		if (Vector3.Distance (myTransform.position, targetedPosition) > 2) {
			targetDestination = false;
		} else {
			targetDestination = true;
		}
		if(!targetDestination)
			myTransform.Translate (Vector3.forward*speed);

		yield return new WaitForSeconds (1f);
		if (transformationOn) {
			speed = 0.8f;
			attackPower = 15;
		} else {
			speed = 1.5f;
			attackPower = 10;
		}
		isHabilityActive = false;
		habilityTwoOn = false;
	}

	//mehtod for setting idle sound
	void SetIdleSound(){
		aSource.clip = idleSound;
		aSource.loop = true;
		aSource.Play ();
	}
	void StopAll(){
		anim.enabled = false;
		aSource.enabled = false;
		this.enabled = false;
	}

	//for additional calculations if needed
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerHealth> ().TakeDamage (attackPower);

			if (col.gameObject.GetComponent<PlayerHealth> ().currentHealth<=0) {
				StopAll ();
			}
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Fire") {
			TakeDamage (5);
		}
		else if (col.gameObject.tag == "Fire2"&&!paralized) {
			TakeParalisysDamage(5);
			if (paralisysdHealth <= 0){
				paralized = true;
		}
	}
}
}