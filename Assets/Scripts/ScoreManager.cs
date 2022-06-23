using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int m_BestScore { private set; get; }
    public int m_Rings { private set; get; }
    public int m_LevelOfFinalBrick;

    public Text m_BestScoreText;
    public Text m_ScoreText;
    
    private void Awake()
    {
        Instance = this;
        //m_LevelOfFinalBrick = PlayerPrefs.GetInt("level_of_final_brick", 1);
    }

    private void Start()
    {
        m_BestScore = PlayerPrefs.GetInt("best_score", 0);
        m_BestScoreText.text = m_BestScore.ToString();

        m_ScoreText.text = m_LevelOfFinalBrick.ToString();
    }

    public void AddRingToInventory(int value)
    {
        if (value > 0)
            m_Rings += value;

        PlayerPrefs.SetInt("rings", m_Rings);
    }

    public void UpdateScore()
    {
        if (m_LevelOfFinalBrick > m_BestScore)
        {
            m_BestScore = m_LevelOfFinalBrick;
            m_BestScoreText.text = m_BestScore.ToString();

            PlayerPrefs.SetInt("best_score", m_BestScore);
        }

        m_ScoreText.text = m_LevelOfFinalBrick.ToString();
    }
}