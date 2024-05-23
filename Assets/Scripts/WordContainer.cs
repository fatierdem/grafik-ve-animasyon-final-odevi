using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WordContainer : MonoBehaviour
{
    private LetterContainer[] letterContainers;
    private int currentLetterIndex;
    private void Awake()
    {
        letterContainers = GetComponentsInChildren<LetterContainer>();
    }

    public void InitializeLetterContainers()
    {
        currentLetterIndex = 0;
        for (int i = 0; i < letterContainers.Length; i++)
        {
            letterContainers[i].InitializeLetterContainers();
        }
    }

    public void Add(char letter)
    {
        letterContainers[currentLetterIndex].SetLetter(letter);
        currentLetterIndex++;
    }

    public void AddAsHint(int letterIndex, char letter)
    {
        letterContainers[letterIndex].SetLetter(letter,true);
    }

    public bool RemoveLetter()
    {
        if(currentLetterIndex <= 0)
        {
            return false;
        }
        currentLetterIndex--;
        letterContainers[currentLetterIndex].InitializeLetterContainers();

        return true;
    
    }
    public string GetWord()
    {
        string word = "";

        for (int i = 0; i < letterContainers.Length; i++)
        {
            word += letterContainers[i].GetLetter().ToString();
        }
        return word;

    }
    public void Colorize(string targetWord)
    {
        List<char> chars = new List<char>(targetWord.ToCharArray());

        for (int i = 0; i < letterContainers.Length; i++)
        {
            char letterCheck = letterContainers[i].GetLetter();
            if (letterCheck == targetWord[i])
            {
                letterContainers[i].SetValid();
                chars.Remove(letterCheck);
            }
            else if (chars.Contains(letterCheck))
            {
                letterContainers[i].SetPotential();
                chars.Remove(letterCheck);
            }
            else
            {
                letterContainers[i].SetInvalid();
            }
        
        }




    }
    public bool IsWordComplete()
    {
        return currentLetterIndex >= 5;
    }


}
