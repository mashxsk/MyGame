using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;

    private int score = 0;
    private float scoreTimer = 0f;
    private float scoreInterval = 1f;
    private bool isDead = false;

    private void Start()
    {
        UpdateScoreText();
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (isDead) return;

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreInterval)
        {
            score++;
            UpdateScoreText();
            scoreTimer = 0f;
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle") && !isDead)
        {
            isDead = true;
            gameObject.SetActive(false); 
            gameOverPanel.SetActive(true); 
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
