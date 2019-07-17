using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
    public static BallLauncher Instance;

    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;
    private Vector3 m_WorldPosition;

    private Vector3 m_Direction;

    private LineRenderer m_LineRenderer;

    private Vector3 m_DefaultStartPosition;

    public SpriteRenderer m_BallSprite;

    public bool m_CanPlay = true;
    [SerializeField] private GameObject m_DeactivatableChildren;

    [Header("Linerenderer Colors")]
    public Color m_CorrectLineColor;    // it will be displayed for correct angles
    public Color m_WrongLineColor;      // it will be displayed for wrong angles

    [Header("Balls")]
    public int m_BallsAmount;
    public int m_TempAmount = 0;  // for score balls
    public Text m_BallsText;
    [SerializeField] private int m_StartingBallsPoolAmount = 10;
    [SerializeField] private Ball m_BallPrefab;
    [SerializeField] private List<Ball> m_Balls;

    [Header("UI Elements")]
    [SerializeField] private GameObject m_ReturnBallsButton;

    private void Awake()
    {
        Instance = this;
        m_CanPlay = true;
        m_LineRenderer = GetComponent<LineRenderer>();

        m_DefaultStartPosition = transform.position;

        m_BallsAmount = PlayerPrefs.GetInt("balls", 1);
    }

    private void Start()
    {
        m_Balls = new List<Ball>(m_StartingBallsPoolAmount);
        
        SpawNewBall(m_StartingBallsPoolAmount);
    }

    private void Update()
    {
        if (GameManager.Instance.m_GameState == GameManager.GameState.MainMenu || GameManager.Instance.m_GameState == GameManager.GameState.GameOver)
            return;

        if (!m_CanPlay)
            return;

        if(Time.timeScale != 0 && GameManager.Instance.m_GameState != GameManager.GameState.GameOver)
            m_WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

        if (Input.GetMouseButtonDown(0))
            StartDrag(m_WorldPosition);
        else if (Input.GetMouseButton(0))
            ContinueDrag(m_WorldPosition);
        else if (Input.GetMouseButtonUp(0))
            EndDrag();
    }

    private void StartDrag(Vector3 worldPosition)
    {
        m_StartPosition = worldPosition;
    }
    
    private void ContinueDrag(Vector3 worldPosition)
    {
        Vector3 tempEndposition = worldPosition;

        Vector3 tempDirection = tempEndposition - m_StartPosition;
        tempDirection.Normalize();

        // getting the angle in radians. you can replace 1.35f with any number or without hardcode like this
        if (Mathf.Abs(Mathf.Atan2(tempDirection.x, tempDirection.y)) < 1.35f)
        {
            Debug.Log("Color is correct");
            m_LineRenderer.startColor = m_CorrectLineColor;
            m_LineRenderer.endColor = m_CorrectLineColor;
        }
        else
        {
            Debug.Log("Color is incorrect");
            m_LineRenderer.startColor = m_WrongLineColor;
            m_LineRenderer.endColor = m_WrongLineColor;
        }

        m_EndPosition = worldPosition;

        m_LineRenderer.SetPosition(1, m_EndPosition - m_StartPosition);
    }

    private void EndDrag()
    {
        if (m_StartPosition == m_EndPosition)
            return;

        m_Direction = m_EndPosition - m_StartPosition;
        m_Direction.Normalize();

        m_LineRenderer.SetPosition(1, Vector3.zero);

        if (Mathf.Abs(Mathf.Atan2(m_Direction.x, m_Direction.y)) < 1.35f)   // hardcode for this time. fix it!
        {
            if(m_Balls.Count < m_BallsAmount)
                SpawNewBall(m_BallsAmount - m_Balls.Count);

            m_CanPlay = false;
            StartCoroutine(StartShootingBalls());
        }
    }

    public void OnMainMenuActions()
    {
        m_CanPlay = false;
        m_BallsAmount = 1;

        m_BallsText.text = "x" + m_BallsAmount.ToString();

        m_BallSprite.enabled = true;
        m_DeactivatableChildren.SetActive(true);

        transform.position = m_DefaultStartPosition;
        m_BallSprite.transform.position = m_DefaultStartPosition;

        ResetPositions();

        m_TempAmount = 0;

        Ball.ResetReturningBallsAmount();

        m_ReturnBallsButton.SetActive(false);

        HideAllBalls();
    }

    public void ResetPositions()
    {
        m_StartPosition = Vector3.zero;
        m_EndPosition = Vector3.zero;
        m_WorldPosition = Vector3.zero;
    }

    private void HideAllBalls()
    {
        for (int i = 0; i < m_Balls.Count; i++)
        {
            m_Balls[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            m_Balls[i].Disable();
        }
    }

    private void SpawNewBall(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            m_Balls.Add(Instantiate(m_BallPrefab, transform.parent, false));
            m_Balls[m_Balls.Count - 1].transform.localPosition = transform.localPosition;
            m_Balls[m_Balls.Count - 1].transform.localScale = transform.localScale;
            m_Balls[m_Balls.Count - 1].Disable();
        }
    }

    IEnumerator StartShootingBalls()
    {
        m_ReturnBallsButton.SetActive(true);
        m_BallSprite.enabled = false;

        int balls = m_BallsAmount;
        
        for (int i = 0; i < m_BallsAmount; i++)
        {
            if (m_CanPlay)
                break;

            m_Balls[i].transform.position = transform.position;
            m_Balls[i].GetReadyAndAddForce(m_Direction);
            
            balls--;
            m_BallsText.text = "x" + balls.ToString();
            
            yield return new WaitForSeconds(0.05f);
        }

        if(balls <= 0)
            m_DeactivatableChildren.SetActive(false);
    }

    public void ActivateHUD()
    {
        m_BallsAmount += m_TempAmount;

        // avoiding more balls than final brick level
        if (m_BallsAmount > BrickSpawner.Instance.m_LevelOfFinalBrick)
            m_BallsAmount = BrickSpawner.Instance.m_LevelOfFinalBrick;

        m_TempAmount = 0;

        m_BallsText.text = "x" + m_BallsAmount.ToString();
        m_DeactivatableChildren.SetActive(true);
        m_ReturnBallsButton.SetActive(false);
    }

    public void ReturnAllBallsToNewStartPosition()
    {
        //StopAllCoroutines();

        if(Ball.s_FirstCollisionPoint != Vector3.zero)
        {
            transform.position = Ball.s_FirstCollisionPoint;
            Ball.ResetFirstCollisionPoint();
        }

        m_BallSprite.transform.position = transform.position;
        m_BallSprite.enabled = true;

        for (int i = 0; i < m_Balls.Count; i++)
        {
            m_Balls[i].DisablePhysics();
            m_Balls[i].MoveTo(transform.position, iTween.EaseType.easeInOutQuart, (Vector2.Distance(transform.position, m_Balls[i].transform.position) / 6.0f), "DeactiveSprite");
        }

        ResetPositions();

        Ball.ResetReturningBallsAmount();

        ScoreManager.Instance.UpdateScore();

        BrickSpawner.Instance.MoveDownBricksRows();
        BrickSpawner.Instance.SpawnNewBricks();

        ActivateHUD();
        m_CanPlay = true;
    }

    public void IncreaseBallsAmountFromOutSide(int amout)
    {
        m_BallsAmount += amout;
        m_BallsText.text = "x" + m_BallsAmount.ToString();
    }
}