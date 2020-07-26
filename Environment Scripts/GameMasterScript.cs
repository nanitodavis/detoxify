using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {

	GameObject[] enemyList;
	GameObject[] enemySpawnersList;
	bool bossAppear = true;
	public GameObject boss;
	public AudioSource aSource;
	public AudioClip bossTheme;
	public bool spwanerActive = true;
	public int maxEnemiesOnScreen;

	// Use this for initialization
	void Start () {
		aSource = GetComponent<AudioSource> ();
		maxEnemiesOnScreen = 15;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, new Vector3 (116, 100, 131), Color.red);
		enemyList = GameObject.FindGameObjectsWithTag ("enemy");
		enemySpawnersList = GameObject.FindGameObjectsWithTag ("spawner");
			if (enemyList.Length <= 0 && enemySpawnersList.Length <= 0) {
				CallBoss ();
			}
	}

	void DissableAll(){
		for (int i = 0; i < enemySpawnersList.Length; i++) {
			enemySpawnersList[i].SetActive(false);
		}
		spwanerActive = false;
	}

	void EnableAll(){
		for (int i = 0; i < enemySpawnersList.Length; i++) {
			enemySpawnersList[i].SetActive(true);
		}
		spwanerActive = true;
	}

	void CallBoss(){
		if(bossAppear){
			aSource.Stop();
			aSource.clip = bossTheme;
			aSource.Play ();
			Instantiate (boss, transform.position, boss.transform.rotation);
			bossAppear = false;
		}
	}
}
