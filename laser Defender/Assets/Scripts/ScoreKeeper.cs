using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
    public int GetCurrentScore()
    {
        return score;
    }
    public void ModifyScore(int addScore)
    {
        score += addScore;
        Mathf.Clamp(score, 0, int.MaxValue);
    }
    public void ResetScore()
    {
        score = 0;
    }
}
