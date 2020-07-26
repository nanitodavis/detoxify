using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

	public GameObject enemyType1;
	public GameObject enemyType2;
	public GameObject enemyType3;
	Vector3 initialPosition;
	public Transform target;
	public float spawnRange;
	public int enemyCuantity;
	public int enemyCount;
	public float spawnDelay = 3f;
	private int enemyToSpawn=0;
	private float spawnTime = 0;
	private float auxiliar = 3f;


	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		spawnTime += Time.deltaTime;
		if (Vector3.Distance (transform.position, target.position) <= spawnRange) {
			if ((enemyCount < enemyCuantity)&&spawnTime>auxiliar) {
				enemyToSpawn = Random.Range (0, 4);
				if (enemyToSpawn <= 1) {
					SpawnEnemiesType1 ();
				} else if (enemyToSpawn > 1 && enemyToSpawn <= 2) {
					SpawnEneiesType2 ();
				} else {
					SpawnEneiesType3 ();
				}
				auxiliar = spawnTime + spawnDelay;
			}
		} 
		if (enemyCount >= enemyCuantity) {
			Destroy (this.gameObject);
		}
	}

	void SpawnEnemiesType1 (){
		Instantiate (enemyType1, initialPosition, Quaternion.identity);
		enemyCount++;
	}

	void SpawnEneiesType2(){
		Instantiate (enemyType2, initialPosition, Quaternion.identity);
		enemyCount++;
	}

	void SpawnEneiesType3(){
		Instantiate (enemyType3, initialPosition, Quaternion.identity);
		enemyCount++;
	}
}
