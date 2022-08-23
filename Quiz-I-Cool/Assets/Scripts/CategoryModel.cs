using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CategoryModel
{
    [SerializeField] string categoryName="";
    [SerializeField] List<QuestionModel> categoryQuestion;
    public string GetCategoryName()
    {
        return categoryName;
    }
    public List<QuestionModel> GetCategoryQuestionList()
    {
        return categoryQuestion;
    }
}
