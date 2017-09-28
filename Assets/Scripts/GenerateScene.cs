using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScene : MonoBehaviour {

    public GameObject greenApple;
    public GameObject redApple;
    public GameObject blueFlower;
    public GameObject yellowFlower;
    public GameObject[] respawnsApple;
    public GameObject[] respawnsFlower;

    // Use this for initialization
    void Start() {

        spawnApples();
        spawnFlowers();

    }

    void spawnFlowers() {
        if (respawnsFlower.Length < 1)
        {
            respawnsFlower = GameObject.FindGameObjectsWithTag("SpawnPointFlower");
        }

        foreach (GameObject respawnFlower in respawnsFlower)
        {
            //radomly choose to spawn a blue or yellow flower
            int flowerToSpawn = Random.Range(0, 2) * 2 - 1;
            if (flowerToSpawn < 0)
            {
                Instantiate(yellowFlower, respawnFlower.transform.position, respawnFlower.transform.rotation);
            }
            else
            {
                Instantiate(blueFlower, respawnFlower.transform.position, respawnFlower.transform.rotation);
            }

        }
    }

    void spawnApples() {
        if (respawnsApple.Length < 1)
        {
            respawnsApple = GameObject.FindGameObjectsWithTag("SpawnPointApple");
        }

        foreach (GameObject respawnApple in respawnsApple)
        {
            //radomly choose to spawn a red or green apple
            int appleToSpawn = Random.Range(0, 2) * 2 - 1;
            if (appleToSpawn < 0)
            {
                Instantiate(greenApple, respawnApple.transform.position, respawnApple.transform.rotation);
            }
            else
            {
                Instantiate(redApple, respawnApple.transform.position, respawnApple.transform.rotation);
            }

        }
    }
	
}
