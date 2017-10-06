using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wilberforce;
using TMPro;

public class GameLogic2 : MonoBehaviour {

    public int ripeApples; //total ripe apples
    public int collectedApples; //ripe apples collected

    public GameObject playCanvas;
    public GameObject explanationCanvas;

    public int normalScore = 0;
    public int colorblindScore = 0;

    public GenerateScene fruitGenerator;

    public GameObject scoreSign; //score sign model
    public GameObject timeSign;  //time sign model

    public Canvas scoreCanvas;
    public Canvas timeCanvas;

    public TextMeshProUGUI timeText; //time text pro UGUI
    public TextMeshProUGUI scoreText; //score text pro UGUI

    public GameObject parkSign; // info screen sign
    public GameObject[] canvasObjs; //all canvas gameobjects

    public GameObject yuckObject; // fail noise
    public GameObject yumObject; // success noise
    
    public Camera mainCamera;

    public bool gameRunning;

    public float timeSetting = 10.0f;
    public float timeRemaining; //length of timer

    public int level = 0;

    //private vars
    private AudioSource yuck;
    private AudioSource yum;

    void Start()
    {
        yum = yumObject.GetComponent<AudioSource>();
        yuck = yuckObject.GetComponent<AudioSource>();

        StartGame();
    }

    void StartGame()
    {
        level = 0;

        //disable timer/score signs
        scoreSign.SetActive(false);
        timeSign.SetActive(false);

        //show park sign
        parkSign.SetActive(true); //sign play button starts gameNormal

        //show explanation canvas
        canvasObjs[0].SetActive(true);
        canvasObjs[1].SetActive(false);
        canvasObjs[2].SetActive(false);
        canvasObjs[3].SetActive(false);
    }

    public void gameNormal()
    {
        //generate fruit
        fruitGenerator.spawnApples();
        fruitGenerator.spawnFlowers();

        //set score
        collectedApples = 0;

        //set possible score
        ripeApples = GameObject.FindGameObjectsWithTag("RipeApple").Length;

        //set timer
        timeRemaining = timeSetting;

        //turn off park sign
        parkSign.SetActive(false);

        //turn on score sign
        scoreSign.SetActive(true);
        timeSign.SetActive(true);

        gameRunning = true;
        level++;
    }

    public void gameColorblind()
    {
        //remove old fruit
        collectedApples = 0;
        
        //generate fruit
        fruitGenerator.spawnApples();
        fruitGenerator.spawnFlowers();

        //set timer
        timeRemaining = timeSetting;

        //turn off park sign
        parkSign.SetActive(false);

        //turn on score sign
        scoreSign.SetActive(true);
        timeSign.SetActive(true);

        //turn on protanopia vision
        Colorblind colorblindsetting = mainCamera.GetComponent<Colorblind>();
        colorblindsetting.Type = 2;

        gameRunning = true;
        level++;
    }

    public void StopGame(int endedLevel)
    {
        if (level==1)//this is normal mode
        {
            normalScore = collectedApples;
        }
        else {//this is colorblind mode
            colorblindScore = collectedApples;
        }

        fruitGenerator.destroyFruit();

        gameRunning = false;

        //turn onpark sign
        parkSign.SetActive(true);

        //turn on score sign
        scoreSign.SetActive(false);
        timeSign.SetActive(false);

        if (endedLevel == 1)
        {
            normalScore = collectedApples;

            //show red-green canvas
            canvasObjs[0].SetActive(false);
            canvasObjs[1].SetActive(false);
            canvasObjs[2].SetActive(true);
            canvasObjs[3].SetActive(false);
        }
        else if (endedLevel == 2)
        {
            colorblindScore = collectedApples;

            //show exit canvas
            canvasObjs[0].SetActive(false);
            canvasObjs[1].SetActive(false);
            canvasObjs[2].SetActive(false);
            canvasObjs[3].SetActive(true);

            //turn on normal vision
            Colorblind colorblindsetting = mainCamera.GetComponent<Colorblind>();
            colorblindsetting.Type = 0;
        }
    }

    void FixedUpdate()
    {
        if (gameRunning)
        {
            DecreaseTime();
        } 
    }

    void DecreaseTime()
    {
        if (timeRemaining > 0.0f)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            StopGame(level);
        }

       UpdateTimer();
    }

    void UpdateTimer()
    {
        timeText.SetText("Time: " + timeRemaining.ToString("F2"));
    }

    void UpdateScore()
    {
        scoreText.SetText("Score: " + collectedApples);
    }

    // Triggered on click:
    // If apple is ripe, increment collectedApples, give positive audio feedback,
    // Else give negative audio feedback.
    // update updateHUD.
    public void OnAppleClick(GameObject other)
    {
        if (other.gameObject.CompareTag("RipeApple"))
        {
            // play "yum" noise
            yum.Play();

            collectedApples++;
            if (collectedApples == ripeApples)
            {
                Debug.Log("All apples collected");
                StopGame(level);
            }
        }
        else if (other.gameObject.CompareTag("UnripeApple"))
        {
            // play "yuck" noise
            yuck.Play();

            // TODO: assign some sort of penalty, like decreasing time
        }

        UpdateScore();
    }

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
