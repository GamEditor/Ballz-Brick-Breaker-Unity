using UnityEngine;

public class ScoreRing : MonoBehaviour
{
    public BricksRow m_Parent;

    private void Awake()
    {
        m_Parent = GetComponentInParent<BricksRow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager.Instance.AddRingToInventory(1);
        gameObject.SetActive(false);
        m_Parent.CheckBricksActivation();
    }
}