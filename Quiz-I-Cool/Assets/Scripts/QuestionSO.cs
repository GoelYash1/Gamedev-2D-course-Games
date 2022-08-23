using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "QuestionSO", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [SerializeField] List<CategoryModel> categories;
    public List<CategoryModel> GetCategoryList()
    {
        return categories;
    }
}
