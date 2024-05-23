using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button submitButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    private int currentWordContainerIndex;
    private bool canAddLetter = true;
    private bool shouldReset;

    public static Action onLetterAdded;
    public static Action onLetterRemoved;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitializeLetterContainers();
        KeyboardKey.onKeyPressedEvent += KeyPressedCallback;
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        KeyboardKey.onKeyPressedEvent -= KeyPressedCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallBack;

    }
    
    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                if (shouldReset)
                {
                    InitializeLetterContainers();
                }
                break;
            
            case GameState.LevelComplete:
                shouldReset = true;
                break;
            case GameState.Gameover:
                shouldReset = true;
                break;
        }
    }
    private void InitializeLetterContainers()
    {
        currentWordContainerIndex = 0;
        canAddLetter = true;

        DisableSubmitButton();
        
        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].InitializeLetterContainers();
        shouldReset = false;
        }
    }

    private void KeyPressedCallback(char letter)
    {
        if (!canAddLetter)
        {
            return;
        }
        wordContainers[currentWordContainerIndex].Add(letter);
        if (wordContainers[currentWordContainerIndex].IsWordComplete())
        {
            canAddLetter = false;
            EnableSubmitButton();
        }
        onLetterAdded?.Invoke();
    }

    public void CheckWord()
    {
        string wordCheck = wordContainers[currentWordContainerIndex].GetWord();
        string targetWord = WordManager.instance.GetTargetWord();

        wordContainers[currentWordContainerIndex].Colorize(targetWord);
        keyboardColorizer.Colorize(targetWord, wordCheck);
        if(wordCheck == targetWord)
        {
            SetLevelComplete();
        }
        else
        {
            //Debug.Log("Wrong Word");

            canAddLetter = true;
            DisableSubmitButton();
            currentWordContainerIndex++;

            if(currentWordContainerIndex >= wordContainers.Length)
            {
                //Debug.Log("Gameover");
                DataManager.instance.ResetScore();
                GameManager.instance.SetGameState(GameState.Gameover);
            }
            else
            {
                canAddLetter = true;
            }

        }
    
    }

    private void SetLevelComplete()
    {
        UpdateData();
        GameManager.instance.SetGameState(GameState.LevelComplete);
    }
    
    private void UpdateData()
    {
        int scoreAdd = 6 - currentWordContainerIndex;

        DataManager.instance.IncreaseScore(scoreAdd);
        DataManager.instance.AddCoins(scoreAdd *3);
    }
    public void BackspacePressedEvent()
    {
        if (!GameManager.instance.IsGameState())
        {
            return;
        }
        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
        if (removedLetter)
        {
            DisableSubmitButton();
        }
        
        canAddLetter = true;
    
    }

    private void EnableSubmitButton()
    {
        submitButton.interactable = true;
    }
    private void DisableSubmitButton()
    {
        submitButton.interactable = false;
    }

    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWordContainerIndex];
    }
}
 