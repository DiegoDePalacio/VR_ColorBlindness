using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    private int ripeApples; //total ripe apples
    private int collectedApples; //ripe apples collected

    public Text signText;

    private float timeLeft = 60.0f;

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

    //triggered on click:
    //if apple is ripe, add to collectedApples and positive audio feedback
    //else give negative audio feedback
    public void OnAppleClick(GameObject other)
    {
        if (other.gameObject.CompareTag("RipeApple"))
        {
            collectedApples++;
            UpdateHUD();
        }
    }

    void UpdateHUD()
    {
        signText.text = "Ripe apples collected:" + collectedApples.ToString();

        if (collectedApples == ripeApples)
        {
            signText.text = "You collected all the ripe apples!";
        }
    }

}
