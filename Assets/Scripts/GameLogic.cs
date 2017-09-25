using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    private int ripeApples; //total ripe apples
    private int collectedApples; //ripe apples collected

    public Text signText; //live game info on sign
    public Text timerText; //live timer info on sign

    public AudioSource sound; // Audio source on player camera
    public AudioClip yuck; // success noise
    public AudioClip yum; // failure noise

    bool gameRunning;

    public float timeLeft = 60.0f; //countdown once game starts

    // Use this for initialization 
    void Start()
    {
        ripeApples = GameObject.FindGameObjectsWithTag("RipeApple").Length;
        collectedApples = 0;
        StartGame();
    }

    // Update is called once per frame //FixedUpdate is called once per second
    void FixedUpdate()
    {
        if (gameRunning == true)
        {
            while (timeLeft > 0.0f)
            {
                timeLeft-= Time.deltaTime;
                UpdateHUD();
            }
        }

    }

    // Triggered on click:
    // If apple is ripe, increment collectedApples, give positive audio feedback,
    // and update updateHUD.
    // Else give negative audio feedback.
    public void OnAppleClick(GameObject other)
    {
        if (other.gameObject.CompareTag("RipeApple"))
        {
            sound.clip = yum;
            sound.Play();
            collectedApples++;
        } else if (other.gameObject.CompareTag("UnripeApple"))
        {
            // play "yuck" noise
            sound.clip = yuck;
            sound.Play();

            //assign some sort of penalty, like decreasing time
        }

        UpdateHUD();
    }

    // Updates sign text.
    // If player collects all apples, end game.
    void UpdateHUD()
    {
        if (gameRunning == true)
        {
            signText.text = "Ripe apples collected:" + collectedApples.ToString();
            timerText.text = timeLeft.ToString();

            if (collectedApples == ripeApples)
            {
                signText.text = "You collected all the ripe apples!";
            }
        }
    }

    // Starts countdown. Maybe makes apples collectable?
    void StartGame()
    {
        gameRunning = true;
        UpdateHUD();
    }

}
