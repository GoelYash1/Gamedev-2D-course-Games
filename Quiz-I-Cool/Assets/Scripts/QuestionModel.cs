using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestionModel
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter the new Question here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public string GetAnswer(int index)
    {
        return answers[index];
    }
    public string GetQuestion()
    {
        return question;
    }
}
