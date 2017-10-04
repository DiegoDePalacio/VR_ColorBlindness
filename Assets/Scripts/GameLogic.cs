using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wilberforce;

public class GameLogic : MonoBehaviour
{

    private int ripeApples; //total ripe apples
    private int collectedApples; //ripe apples collected

    //public Transform[] spawnPoints;
    public GameObject ripeApple;
    public GameObject unripeApple;

    public GameObject playCanvas;
    public GameObject explanationCanvas;
    public GameObject parkSign;

    public Text signText; //live game info on sign
    public Text timerText; //live timer info on sign

    public AudioSource sound; // Audio source on player camera
    public AudioClip yuck; // success noise
    public AudioClip yum; // failure noise

    public bool gameRunning = false;

    public float timeLeft; //countdown once game starts

    public Colorblind colorBlindSetting;

    // Use this for initialization 
    void Start()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        colorBlindSetting = mainCamera.GetComponent<Colorblind>();
        // Deprecated by GenerateScene.cs
        //GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
        //int i = 0;
        //foreach (GameObject spawn in spawns) //assign spawn transforms to spawnPoints array
        //{
        //    spawnPoints[i] = spawn.transform;
        //    i++;
        //}
       
        sound = mainCamera.GetComponent<AudioSource>();
        //SpawnApples(spawnPoints);
        ripeApples = GameObject.FindGameObjectsWithTag("RipeApple").Length;
        collectedApples = 0;
        //StartGame();

    }

    // Update is called once per frame //FixedUpdate is called once per second
    void FixedUpdate()
    {
        if (gameRunning == true)
        {
            DecreaseTime();
            if(collectedApples > ripeApples / 3) // if the player has collected more than 1/3 of the red apples, switch the colorblind setting to 2: Deuteranopia
            {
                // colorBlindSetting.Type = Mathf.RoundToInt(Random.Range(0.0f, 3.0f));
                colorBlindSetting.Type = 2;
            }
        }
        UpdateHUD();

    }

    // Triggered on click:
    // If apple is ripe, increment collectedApples, give positive audio feedback,
    // Else give negative audio feedback.
    // update updateHUD.
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
            timerText.text = "Time: " + timeLeft.ToString("F0");

            if (collectedApples == ripeApples)
            {
                signText.text = "You collected all the ripe apples!";
                gameRunning = false;
            }
        }
    }

    // Starts countdown. Maybe makes apples collectable?
    public void StartGame()
    {
        parkSign.SetActive(false);
        gameRunning = true;
        UpdateHUD();
    }

    private void DecreaseTime()
    {
        if(timeLeft > 0.0f)
        {
            timeLeft-= Time.deltaTime;
        } else
        {
            gameRunning = false;
        }
        UpdateHUD();
    }

    //public void SpawnApples(Transform[] spawnPoints)
    //{
    //    foreach (Transform spawnPoint in spawnPoints)
    //    {
    //        float typeOfApple = Random.value;
    //        if(typeOfApple < 0.5f)
    //        {
    //            Instantiate(unripeApple, spawnPoint.position, spawnPoint.rotation);
    //        } else
    //        {
    //            Instantiate(ripeApple, spawnPoint.position, spawnPoint.rotation);
    //        }
    //    }
    //}

    public void NextSlide(string slideName)
    {
        if (slideName == "PlaySlide")
        {
            explanationCanvas.SetActive(false);
            playCanvas.SetActive(true);
        }
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

}
