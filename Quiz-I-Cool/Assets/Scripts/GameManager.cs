using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] QuestionSO questionBank;
    List<CategoryModel> categories;
    [SerializeField] int selectedCategoryIndex = -1;
    [SerializeField] int selectedDifficultyIndex = 0; 
    bool isGameComplete= false;
    bool isCategoryChosen = false;
    void Awake()
    {
        categories = questionBank.GetCategoryList();
    }
    public void SetCategoryIndex(int index)
    {
        if(index>-1 && index < categories.Count)
        {
            selectedCategoryIndex = index;
            return;
        }
        Debug.Log("No Category has been selected");
    }
    public bool IsCategorySelected()
    {
        if(selectedCategoryIndex > -1 && selectedCategoryIndex < categories.Count)
        {
            isCategoryChosen = true;
            return isCategoryChosen;
        }
        else
        {
            return false;
        }
    }
    public int GetSelectedCategoryIndex()
    {
        return selectedCategoryIndex;
    }
    public void SetDifficultyIndex(int index)
    {
        if(index > -1 && index <3)
        {
            selectedDifficultyIndex = index;
            return;
        }
        Debug.Log("No Difficulty has been selected");
    }
    public float GetTimeToCompleteQuestion()
    {
        if(selectedDifficultyIndex == 0)
        {
            return 25.0f;
        }
        else if(selectedDifficultyIndex == 1)
        {
            return 20.0f;
        }
        else
        {
            return 15.0f;
        }
    }
    public List<QuestionModel> GetQuestionList()
    {
        if (selectedCategoryIndex > -1 && selectedCategoryIndex < categories.Count)
        {

            return categories[selectedCategoryIndex].GetCategoryQuestionList();
        }
        else
        {
            Debug.Log("No Category Selected");
            return null;
        }
    }
    public int GetQuestionsRemaining()
    {
        if(selectedDifficultyIndex == 0)
        {
            return 5;
        }
        else if(selectedDifficultyIndex == 1)
        {
            return 10;
        }
        else
        {
            return 15;
        }
    }
    public void CheckGameState(bool check)
    {
        isGameComplete = check;
    }
    public bool GameComplete()
    {
        return isGameComplete;
    }
    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
