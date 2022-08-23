using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCanvas : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] List<Sprite> backgroundSprites= new List<Sprite>();
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    public void SetBackgroundImage()
    {
        GetComponentInChildren<Image>().sprite = backgroundSprites[gameManager.GetSelectedCategoryIndex()];
    }
}
