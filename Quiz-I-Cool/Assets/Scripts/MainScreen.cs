using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void ChosenCategoryIndex(int index)
    {
        gameManager.SetCategoryIndex(index);
    }
    public void ChosenDifficultyIndex(Slider difficultySlider)
    {
        gameManager.SetDifficultyIndex((int)difficultySlider.value);
    }
}
