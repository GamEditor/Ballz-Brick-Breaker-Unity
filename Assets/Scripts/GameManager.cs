using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject m_MainMenuPanel;
    public GameObject m_GameMenuPanel;
    public GameObject m_GameOverPanel;
    public GameObject m_Scores;
    public Text m_GameOverFinalScore;

    public enum GameState { MainMenu, }
    private GameState m_State; //= GameState.MainMenu;

    public GameState m_GameState
    {
        set
        {
            m_State = value;

            switch(value)
            {
                case GameState.MainMenu:
                    m_MainMenuPanel.SetActive(true);
                    m_GameMenuPanel.SetActive(false);
                    m_GameOverPanel.SetActive(false);
                    m_Scores.SetActive(true);

                    BallLauncher.Instance.OnMainMenuActions();
                    BrickSpawner.Instance.HideAllBricksRows();
                    break;
            }
        }
        get
        {
            return m_State;
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // m_GameState = GameState.MainMenu;
        Debug.Log("start gameManager gameState " + m_GameState);
        Debug.Log("instanse state " + Instance.m_GameState);
    }

    private void Update()
    {
        
    }
}