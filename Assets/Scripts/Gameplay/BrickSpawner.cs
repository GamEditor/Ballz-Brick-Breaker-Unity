using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public static BrickSpawner Instance;

    public int m_LevelOfFinalBrick;

    [Header("Spawning informations")]
    public int m_SpawningRows = 7;
    public BricksRow m_BricksRowPrefab;
    public float m_SpawningTopPosition = 2.88f;   // top position
    public float m_SpawningDistance = 0.8f; // distance of rows

    [Header("Bricks Row")]
    public List<BricksRow> m_BricksRow;

    private void Awake()
    {
        Instance = this;

        m_LevelOfFinalBrick = PlayerPrefs.GetInt("level_of_final_brick", 1);

        m_BricksRow = new List<BricksRow>();

        // generate rows of bricks on the scene
        for (int i = 0; i < m_SpawningRows; i++)
        {
            m_BricksRow.Add(Instantiate(m_BricksRowPrefab, transform.parent, false));
            m_BricksRow[m_BricksRow.Count - 1].transform.localPosition = new Vector3(0, m_SpawningTopPosition, 0);
            m_BricksRow[m_BricksRow.Count - 1].gameObject.SetActive(false);
        }
    }

    public void HideAllBricksRows()
    {
        for (int i = 0; i < m_BricksRow.Count; i++)
            m_BricksRow[i].gameObject.SetActive(false);
    }

    public void SpawnNewBricks()
    {
        for (int i = 0; i < m_BricksRow.Count; i++)
        {
            if(!m_BricksRow[i].gameObject.activeInHierarchy)
            {
                m_BricksRow[i].gameObject.SetActive(true);
                break;
            }
        }
        
        m_LevelOfFinalBrick++;
    }

    public void MoveDownBricksRows()
    {
        for (int i = 0; i < m_BricksRow.Count; i++)
            if (m_BricksRow[i].gameObject.activeInHierarchy)
                m_BricksRow[i].MoveDown(m_SpawningDistance);
    }
}