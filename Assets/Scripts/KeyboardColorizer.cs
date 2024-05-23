using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    private KeyboardKey[] keys;
    private bool shouldReset;
    private void Start()
    {
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
            case GameState.Game:
                if (shouldReset)
                {
                    Initialize();
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

    private void Initialize()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Initialize();
        }
        shouldReset = false;
    
    }
    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }

    public void Colorize(string targetWord, string wordCheck)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();

            for (int j = 0; j < wordCheck.Length; j++)
            {
                if(keyLetter != wordCheck[j])
                {
                    continue;
                }
                if(keyLetter == targetWord[j])
                {
                    keys[i].SetValid();
                }
                else if (targetWord.Contains(keyLetter))
                {
                    keys[i].SetPotential();
                }
                else
                {
                    keys[i].SetInvalid();
                }
            
            }
        }
    }


}
