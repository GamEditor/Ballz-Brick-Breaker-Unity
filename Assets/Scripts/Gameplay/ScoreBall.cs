using UnityEngine;

public class ScoreBall : MonoBehaviour
{
    private BricksRow m_Parent;
    private ParticleSystem m_ParentParticle;

    private Color m_ParticleColor;

    private void Awake()
    {
        m_Parent = GetComponentInParent<BricksRow>();
        m_ParentParticle = GetComponentInParent<ParticleSystem>();

        m_ParticleColor = new Color(0, 1, 0, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallLauncher.Instance.m_TempAmount++;    // increase balls amount
        PlayParticle();
    }

    public void PlayParticle()
    {
        gameObject.SetActive(false);

        m_ParentParticle.startColor = m_ParticleColor;
        m_ParentParticle.Play();
    }
}