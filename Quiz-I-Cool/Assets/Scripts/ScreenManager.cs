using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    GameManager gameManager;
    MainScreen mainScreen;
    Quiz quiz;
    EndScreen endScreen;
    Timer timer;
    ScoreKeeper scoreKeeper;
    BackgroundCanvas backgroundCanvas;
    void Awake()
    {
        backgroundCanvas = FindObjectOfType<BackgroundCanvas>();
        gameManager = FindObjectOfType<GameManager>();
        mainScreen = FindObjectOfType<MainScreen>();
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        mainScreen.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
        scoreKeeper.gameObject.SetActive(false);
    }
    void Update()
    {
        if (gameManager.IsCategorySelected())
        {

            mainScreen.gameObject.SetActive(false);
            backgroundCanvas.SetBackgroundImage();
            timer.gameObject.SetActive(true);
            scoreKeeper.gameObject.SetActive(true);
            quiz.gameObject.SetActive(true);
        }

        if (gameManager.GameComplete())
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }
}
