using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
	public float currentHealth;
    public Image healthBar;
    public Image damageImage;
	public AudioClip deathClip;
	public AudioClip hurtClip;
    public float flashSpeed = 5f;
    public Color flashColur = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;

    bool isDead;
    bool damaged;

    // Use this for initialization
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update () {
		if (damaged) {
			//damageImage = flashColur;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

    public void TakeDamage ( int amount){
        damaged = true;
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / 100;
		playerAudio.clip = hurtClip;
		playerAudio.volume=1;
        playerAudio.Play();
        if (currentHealth <=0 && !isDead)
        {
            Death();
        }
        
    }

	void Death()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;

		// Turn off any remaining shooting effects.
		//playerShooting.DisableEffects();

		// Tell the animator that the player is dead.
		anim.SetTrigger("Die");

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		playerAudio.volume=1;
		playerAudio.clip = deathClip;
		playerAudio.Play();

		transform.gameObject.GetComponent<LookAtMouse> ().enabled = false;
		transform.gameObject.GetComponent<PlayerMovement> ().enabled = false;
		transform.gameObject.GetComponent<FireScript> ().enabled = false;
		transform.gameObject.GetComponent<Animator> ().enabled = false;
	}
}
