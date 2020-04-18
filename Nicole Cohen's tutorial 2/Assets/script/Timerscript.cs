using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timerscript : MonoBehaviour
{
    public bool isover = false;
    public float startingTime;

    public Text textTime;

    public playerscript playerScript;

    public bool fail;

    void Update()
    {
        if (isover == false)
        {
            startingTime -= Time.deltaTime;
        }



        if (startingTime < 0)
        {
            startingTime = 0;
        }

        Countdown();
        textTime.text = "Time Remaining:" + Mathf.Round(startingTime);

    }

    void Countdown()
    {

        if (playerScript.scoreValue == 8)
        {
            isover = true;
            playerScript.Score();
        }
        if (startingTime == 0 && playerScript.scoreValue != 8)
        {
            playerScript.GameOver();

        }
    }
}
