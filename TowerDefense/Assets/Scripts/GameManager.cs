using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //TODO

    public static bool gameIsOver;

    void Start()
    {
        gameIsOver = false;
    }

    void Update()
    {
        if (gameIsOver) return;

        //TODO End game if player run out of lives
    }

    void EndGame()
    {
        gameIsOver = true;
        //TODO UI
    }

    public void WinLevel()
    {
        gameIsOver = true;
        //TODO UI
    }

}
