using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    [SerializeField] private CanvasGroup menuCanvasG;
    [SerializeField] private CanvasGroup gameCanvasG;
    [SerializeField] private CanvasGroup levelCompleteCanvasG;
    [SerializeField] private CanvasGroup gameoverCanvasG;
    [SerializeField] private CanvasGroup settingCanvasG;


    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;

    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteTargetWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [SerializeField] private TextMeshProUGUI gameoverCoins;
    [SerializeField] private TextMeshProUGUI gameoverTargetWord;
    [SerializeField] private TextMeshProUGUI gameoverBestScore;

    [SerializeField] private TextMeshProUGUI gameScore;
    [SerializeField] private TextMeshProUGUI gameCoins;

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
        ShowMenu();
        HideGame();
        HideLevelComplete();
        HideGameover();

        GameManager.onGameStateChanged += GameStateChangedCallBack;
        DataManager.onCoinsUpdated += UpdateCoinsTexts;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
        DataManager.onCoinsUpdated -= UpdateCoinsTexts;

    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                ShowMenu();
                HideGame();
                HideSettings();
                break;
            
            case GameState.Game:
                ShowGame();
                HideMenu();
                HideLevelComplete();
                HideGameover();
                break;

            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;

            case GameState.Gameover:
                ShowGameOver();
                HideGame();
                break;
        }
    
    }

    private void UpdateCoinsTexts()
    {
        menuCoins.text = DataManager.instance.GetCoin().ToString();
        gameCoins.text = menuCoins.text;
        levelCompleteCoins.text = menuCoins.text;
        gameoverCoins.text = menuCoins.text;
    }

    private void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoin().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCanvasG(menuCanvasG);
    }

    private void HideMenu()
    {
        HideCanvasG(menuCanvasG);
    }
    private void ShowGame()
    {
        gameScore.text = DataManager.instance.GetScore().ToString();
        gameCoins.text = DataManager.instance.GetCoin().ToString();

        ShowCanvasG(gameCanvasG);
    }
    private void HideGame()
    {
        HideCanvasG(gameCanvasG);
    }
    private void ShowLevelComplete()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoin().ToString();
        levelCompleteTargetWord.text = WordManager.instance.GetTargetWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCanvasG(levelCompleteCanvasG);
    }
    private void HideLevelComplete()
    {
        HideCanvasG(levelCompleteCanvasG);
    }

    private void ShowGameOver()
    {
        gameoverCoins.text = DataManager.instance.GetCoin().ToString();
        gameoverTargetWord.text = WordManager.instance.GetTargetWord().ToString();
        gameoverBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCanvasG(gameoverCanvasG);
    }
    
    private void HideGameover()
    {
        HideCanvasG(gameoverCanvasG);
    }

    public void ShowSwttings()
    {
        ShowCanvasG(settingCanvasG);
    }

    public void HideSettings()
    {
        HideCanvasG(settingCanvasG);
    }
    private void ShowCanvasG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    private void HideCanvasG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}

