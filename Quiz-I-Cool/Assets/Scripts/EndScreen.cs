using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void ShowFinalScore()
    {
        if(scoreKeeper.CalculateScore()==100)
        {
            finalScoreText.text = "Wow Perfect Score!\n You got " + scoreKeeper.CalculateScore() + "%";
        }
        else if(scoreKeeper.CalculateScore()>=60 && scoreKeeper.CalculateScore()<100)
        {
            finalScoreText.text = "Congratulations!\n You got " + scoreKeeper.CalculateScore() + "%";
        }
        else
        {
            finalScoreText.text = "Bad Luck!\n You got " + scoreKeeper.CalculateScore() + "%";
        }
    }
}
