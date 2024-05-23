using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    [SerializeField] private GameObject keyboard;

    private KeyboardKey[] keys;
    [SerializeField] private TextMeshProUGUI keyboardPriceText;
    [SerializeField] private TextMeshProUGUI letterPriceText;

    [SerializeField] private int keyboardHintPrice;
    [SerializeField] private int letterHintPrice;


    private bool shouldReset;
    private void Awake()
    {
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();
    }
    private void Start()
    {
        keyboardPriceText.text = keyboardHintPrice.ToString();
        letterPriceText.text = letterHintPrice.ToString();

        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;

    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                break;

            case GameState.Game:
                if (shouldReset)
                {
                    letterHintIndices.Clear();
                    shouldReset = false;
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
    public void KeyboardHint()
    {
        if(DataManager.instance.GetCoin() < keyboardHintPrice)
        {
            return;
        }
        
        string targetWord = WordManager.instance.GetTargetWord();

        List<KeyboardKey> untouchedKeys = new List<KeyboardKey>();
        
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].IsUnTouched())
            {
                untouchedKeys.Add(keys[i]);
            }
        }
        List<KeyboardKey> t_untouchedKeys = new List<KeyboardKey>(untouchedKeys);
        
        for (int i = 0; i < untouchedKeys.Count; i++)
        {
            if (targetWord.Contains(untouchedKeys[i].GetLetter()))
            {
                t_untouchedKeys.Remove(untouchedKeys[i]);
            }
        }
        if (t_untouchedKeys.Count<=0)
        {
            return;
        }

        int randomKeyIndex = Random.Range(0, t_untouchedKeys.Count);
        t_untouchedKeys[randomKeyIndex].SetInvalid();

        DataManager.instance.RemoveCoins(keyboardHintPrice);
    }

    List<int> letterHintIndices = new List<int>();
    public void LetterHint()    
    {
        if(DataManager.instance.GetCoin() < letterHintPrice)
        {
            return;
        }

        if(letterHintIndices.Count >= 5)
        {
            Debug.Log("All hints");
            return;
        }
        List<int> letterHintNotIndices = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            if (!letterHintIndices.Contains(i))
            {
                letterHintNotIndices.Add(i);
            }
        }
        WordContainer currentWordContainer = InputManager.instance.GetCurrentWordContainer();

        string targetWord = WordManager.instance.GetTargetWord();

        int randomIndex = letterHintNotIndices[Random.Range(0, letterHintNotIndices.Count)];
        letterHintNotIndices.Add(randomIndex);

        currentWordContainer.AddAsHint(randomIndex, targetWord[randomIndex]);

        DataManager.instance.RemoveCoins(letterHintPrice);
    }






}
