using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("SceneLogic");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EatApple()
    {
        GameLogic game = gameManager.GetComponent<GameLogic>();
        game.OnAppleClick(gameObject);
        Destroy(gameObject);
    }
}
