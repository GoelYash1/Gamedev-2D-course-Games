using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    AudioManager audioManager;
    GameManager gameManager;
    [Header("Audios")]
    AudioSource audioSource;
    [SerializeField]float clickVolume = 0.9f;
    [SerializeField] AudioClip correctAnswerClip;
    [SerializeField] AudioClip wrongAnswerClip;

    [Header("Questions")]
    List<QuestionModel> questions = new List<QuestionModel>();
    [SerializeField] TextMeshProUGUI questionText;
    List<int> prevQuestionIndex = new List<int>();
    [SerializeField] int questionsRemaining = 5;
    QuestionModel currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Buttons")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite selectedAnswerSprite;

    [Header("Timers")]
    [SerializeField] Image timerImage;
    Timer timerScript;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    bool isComplete;

    [Header("Slider text")]
    [SerializeField] TextMeshProUGUI sliderText;
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        timerScript = FindObjectOfType<Timer>();
        questionsRemaining = gameManager.GetQuestionsRemaining();
        progressBar.maxValue = questionsRemaining;
        progressBar.value = 0;
        questions = gameManager.GetQuestionList();
        audioSource = GetComponent<AudioSource>();
        audioManager.PlayAudio();
    }
    void Update()
    {
        timerImage.fillAmount = timerScript.fillFraction;
        if (timerScript.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                gameManager.CheckGameState(isComplete);
                audioManager.PlayAudio();
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timerScript.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timerScript.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            hasAnsweredEarly = true;
            SetButtonState(false);
        }
    }
    
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timerScript.CancelTimer();
    }
    void GetNextQuestion()
    {
        if (questionsRemaining > 0 && questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            UpdateText();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        while(questions.Count > prevQuestionIndex.Count && prevQuestionIndex.Contains(index))
        {
            index = Random.Range(0, questions.Count);
        }
        prevQuestionIndex.Add(index);
        currentQuestion = questions[index];
        questionsRemaining--;
    }
    void DisplayAnswer(int index)
    {
        if (hasAnsweredEarly)
        {
            Image selectedButtonImage;
            selectedButtonImage = answerButtons[index].GetComponent<Image>();
            selectedButtonImage.sprite = selectedAnswerSprite;
        }
        Image buttonImage;
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
        if (index == correctAnswerIndex)
        {
            GetComponent<AudioSource>().PlayOneShot(correctAnswerClip,clickVolume);
            if (questionsRemaining == 0)
            {
                questionText.text = "Correct! Your score is";
            }
            else
            {
                questionText.text = "Correct! you are doing good";
            }
            scoreKeeper.IncrementCorrectAnswer();
        }
        else if (index == -1)
        {
            GetComponent<AudioSource>().PlayOneShot(wrongAnswerClip,clickVolume);
            if (questionsRemaining == 0)
            {
                questionText.text = "Quiz finished! the correct answer was\n" + correctAnswer;
            }
            else
            {
                questionText.text = "Time ran out! the correct answer was\n" + correctAnswer;
            }
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(wrongAnswerClip,clickVolume);
            if (questionsRemaining == 0)
            {
                questionText.text = "Wrong! The correct answer was " + correctAnswer + "\nLets see your score:";
            }
            else
            {
                questionText.text = "Wrong, better luck in the next question.\nCorrect Answer: " + correctAnswer;
            }
        }
        buttonImage.sprite = correctAnswerSprite;
        scoreText.text = "Score:" + scoreKeeper.CalculateScore() + "%";
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
    void UpdateText()
    {
        sliderText.text = (progressBar.maxValue - questionsRemaining).ToString() +" / " +progressBar.maxValue.ToString();
    }
}
