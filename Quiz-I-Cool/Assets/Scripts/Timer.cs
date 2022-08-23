using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]float timeToShowCorrectAnswer = 5.0f;
    [SerializeField]float timeToCompleteQuestion = 25.0f;
    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion = false;
    float timerValue;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timeToCompleteQuestion = gameManager.GetTimeToCompleteQuestion();
    }
    void Update()
    {
        UpdateTimer();
    }
    public void CancelTimer()
    {
        timerValue = 0;
    }
    
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
