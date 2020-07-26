using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	public AudioClip stoppedClip;                 // The sound to play when the enemy dies.
	public float updateDelay = 10f;


	Animator anim;                              // Reference to the animator.
	AudioSource enemyAudio;                     // Reference to the audio source.
	CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
	[SerializeField]
	bool isStopped;                             // Whether the enemy is dead.
	EnemyAnimation enemyAnim;
	EnemyFollowPlayer enemyFollow;


	void Awake()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		enemyAnim = GetComponent<EnemyAnimation> ();
		enemyFollow = GetComponent<EnemyFollowPlayer> ();

		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	void Update()
	{
		enemyAnim.SetStopped (isStopped);
		enemyFollow.SetStopped (isStopped);
	}

	public void TakeDamage(int amount)
	{
		// If the enemy is dead...
		if (isStopped)
			// ... no need to take damage so exit the function.
			return;

		// Play the hurt sound effect.
		enemyAudio.clip = stoppedClip;
		enemyAudio.Play();

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		// If the current health is less than or equal to zero...
		if (currentHealth <= 0)
		{
			// ... the enemy is dead.
			Cured();
		}
	}


	void Stopped()
	{
		// The enemy is dead.
		isStopped = true;
		enemyAnim.SetStopped (isStopped);

		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		enemyAudio.clip = stoppedClip;
		enemyAudio.Play();

		InvokeRepeating ("StartAgain", 0f, updateDelay);
	}

	public void Cured()
	{
		Destroy (this.gameObject, 0.1f);
	}

	void StartAgain()
	{
		//currentHealth = startingHealth;
		isStopped = false;
		enemyAnim.SetStopped (isStopped);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Fire") {
			TakeDamage (10);
		}
		else if(col.gameObject.tag == "Fire2") {
			
		}
	}

}