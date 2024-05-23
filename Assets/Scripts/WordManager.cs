using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;
    [SerializeField] private string targetWord;
    [SerializeField] private TextAsset wordsText;
    private string words;

    private bool shouldReset;
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
        words = wordsText.text;
    }

    private void Start()
    {
        SetNewTargetWord();
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
                    SetNewTargetWord();
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
    public string GetTargetWord()
    {
        return targetWord.ToUpper();
    }

    private void SetNewTargetWord()
    {
        string[] wordArray = words.Split('\n');

        int wordIndex = Random.Range(0, wordArray.Length);

        targetWord = wordArray[wordIndex].Trim().ToUpper();

        shouldReset = false;
    }

}



