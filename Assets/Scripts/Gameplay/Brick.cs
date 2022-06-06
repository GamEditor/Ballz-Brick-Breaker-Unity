﻿using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
    public Text m_Text;
    public int m_Health;    // it's gonna be public because the GameManager needs to setup each brick
    private PolygonCollider2D polygonCollider2D;

    private SpriteRenderer m_SpriteRenderer;
    private ParticleSystem m_ParentParticle;

    private void Awake()
    {
        polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_ParentParticle = GetComponentInParent<ParticleSystem>();
    }

    private void OnEnable()
    {
        m_Health = BrickSpawner.Instance.m_LevelOfFinalBrick;
        m_Text.text = m_Health.ToString();

        ChangeColor();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            polygonCollider2D.isTrigger = false;
            m_Health--;
            m_Text.text = m_Health.ToString();
            ChangeColor();

            if (m_Health <= 0)
            {
                // 1 - play a particle
                Color color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, 0.5f);
                m_ParentParticle.startColor = color;
                m_ParentParticle.Play();

                // 2 - hide this Brick or this row
                gameObject.SetActive(false);
                //m_Parent.CheckBricksActivation();
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Ball>() != null)
        {
            polygonCollider2D.isTrigger = false;
            m_Health--;
            m_Text.text = m_Health.ToString();
            ChangeColor();

            if (m_Health <= 0)
            {
                // 1 - play a particle
                Color color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, 0.5f);
                m_ParentParticle.startColor = color;
                m_ParentParticle.Play();

                // 2 - hide this Brick or this row
                gameObject.SetActive(false);
                //m_Parent.CheckBricksActivation();
            }
            //polygonCollider2D.isTrigger = true;
        }
    }

    public void Attack ()
    {
        Debug.Log("Attack");
    }
    
    public void ChangeColor()
    {
        m_SpriteRenderer.color = Color.LerpUnclamped(new Color(1, 0.75f, 0, 1), Color.red, m_Health / (float)BrickSpawner.Instance.m_LevelOfFinalBrick);
    }
}