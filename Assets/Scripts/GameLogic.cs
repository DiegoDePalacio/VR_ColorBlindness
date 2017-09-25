using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    private int ripeApples; //total ripe apples
    private int collectedApples; //ripe apples collected

    public Text signText; //live game info on sign

    private float timeLeft = 60.0f; //countdown once game starts

    // Use this for initialization
    void Start()
    {
        ripeApples = GameObject.FindGameObjectsWithTag("RipeApple").Length;
        collectedApples = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Triggered on click:
    // If apple is ripe, increment collectedApples, give positive audio feedback,
    // and update updateHUD.
    // Else give negative audio feedback.
    public void OnAppleClick(GameObject other)
    {
        if (other.gameObject.CompareTag("RipeApple"))
        {
            collectedApples++;
            UpdateHUD();
        }
    }

    // Updates sign text.
    // If player collects all apples, end game.
    void UpdateHUD()
    {
        signText.text = "Ripe apples collected:" + collectedApples.ToString();

        if (collectedApples == ripeApples)
        {
            signText.text = "You collected all the ripe apples!";
        }
    }

    // Starts countdown. Maybe makes OnAppleClick Active?
    void StartGame()
    {

    }

}
